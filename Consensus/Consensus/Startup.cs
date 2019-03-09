using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Consensus.Filters;
using Consensus.Hubs;
using Consensus.Models;
using Consensus.Security;
using ConsensusLibrary.BackgroundProcessService;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.FileContext;
using ConsensusLibrary.UserContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Consensus
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Env = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbSettings = Configuration.GetSection("DbSettings");

            IUserRepository userRepository;
            ICategoryRepository categoryRepository;
            IDebateRepository debateRepository;
            IFileRepository fileRepository;

            if (dbSettings.GetValue<bool>("UseDb"))
            {
                var connectionString = dbSettings.GetValue<string>("ConnectionString");

                userRepository = new InMemoryUserRepository();
                categoryRepository = new InPostgreSqlCategoryRepository(connectionString);
                debateRepository = new InMemoryDebateRepository();
                fileRepository = new InMemoryFileRepository();
            }
            else
            {
                userRepository = new InMemoryUserRepository();
                categoryRepository = new InMemoryCategoryRepository();
                debateRepository = new InMemoryDebateRepository();
                fileRepository = new InMemoryFileRepository();
                FillCategories(categoryRepository);
            }


            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var userProfileFacade = new UserProfileFacade(userRepository);
            var useHangFire = Configuration.GetValue<bool>("UseHangFire");
            var hangFireConneсtionString = Configuration.GetValue<string>("HangFireConnectionString");
            var roundLength = Configuration.GetValue<TimeSpan>("DebateRoundLength");
            var backgroundProcessServiceSettings = new BackgroundProcessServiceSettings(
                Configuration.GetValue<TimeSpan>("DebateAllowedOverdue"), roundLength);
            var paginationSettings = new PaginationSettings(Configuration.GetValue<int>("PageSize"));

            var debateSettings = new DebateSettings(Configuration.GetValue<int>("DebateRoundCount"),
                roundLength);


            var fileSettings = new FileSettings(GenerateUploadPath());
            var fileFacade = new FileFacade(fileRepository, userRepository, fileSettings);

            IBackgroundProcessService backgroundProcessService;

            if (useHangFire)
            {
                backgroundProcessService = new HangFireProcessService(
                    backgroundProcessServiceSettings,
                    debateRepository,
                    hangFireConneсtionString);
            }
            else
            {
                backgroundProcessService = new TimerBackgroundProcessService(
                    backgroundProcessServiceSettings, 
                    debateRepository);
            }

            var debateFacade = new DebateFacade(userRepository, debateRepository,
                debateSettings, backgroundProcessService, categoryRepository);
            var debateVotingFacade = new DebateVotingFacade(debateRepository, userRepository);
            var chatFacade = new ChatFacade(debateRepository, userRepository);
            var categoryFacade = new CategoryFacade(categoryRepository);
            var userSearchFacade = new UserSearchFacade(userRepository, debateRepository, categoryRepository);


            services.AddSingleton<IRegistrationFacade>(registrationFacade);
            services.AddSingleton<IUserSearchFacade>(userSearchFacade);
            services.AddSingleton<IDebateFacade>(debateFacade);
            services.AddSingleton<IDebateVotingFacade>(debateVotingFacade);
            services.AddSingleton<IChatFacade>(chatFacade);
            services.AddSingleton<IUserProfileFacade>(userProfileFacade);

            services.AddSingleton<ICategoryFacade>(categoryFacade);
            services.AddSingleton<IFileFacade>(fileFacade);
            services.AddSingleton(paginationSettings);

            ConfigureSecurity(services);

            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Consensus API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
            });

            var optionModel = new OptionModel
            {
                Secret = Configuration.GetValue<string>("Secret"),
                OpenviduUrl = Configuration.GetValue<string>("OpenviduUrl")
            };

            services.AddSingleton(optionModel);

            services.AddMvc(o => { o.Filters.Add(new ExceptionFilter()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("AllowAnyOrigin");

            app.UseSwagger();

            app.UseSwaggerUI(current => { current.SwaggerEndpoint("/swagger/v1/swagger.json", "Consensus"); });

            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chatHub"); });

            app.UseMvc();
        }

        private void FillCategories(ICategoryRepository repository)
        {
            repository.AddCategory(new Category("Home"));
            repository.AddCategory(new Category("Politics"));
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            var securityConfiguration = Configuration.GetSection("Security");
            var securitySettings = new SecuritySettings(
                securityConfiguration["EncryptionKey"], securityConfiguration["Issue"],
                securityConfiguration.GetValue<TimeSpan>("ExpirationPeriod"));
            var jwtIssuer = new JwtIssuer(securitySettings);
            services.AddSingleton(securitySettings);
            services.AddSingleton<IJwtIssuer>(jwtIssuer);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(securitySettings.EncryptionKey))
                    };
                });

            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy =
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build();

                    options.AddPolicy("AdminOnly",
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.Admin).Build());
                });
        }

        private string GenerateUploadPath()
        {
            var uploadPath = Path.Combine(Env.WebRootPath,
                Configuration.GetValue<string>("UploadDirectory"));

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            return uploadPath;
        }
    }
}