using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Consensus.Filters;
using Consensus.Models;
using Consensus.Security;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.DebateContext;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);

            var debateRepository = new InMemoryDebateRepository();
            var debateFacade = new DebateFacade(userRepository, debateRepository);


            services.AddSingleton<IRegistrationFacade>(registrationFacade);
            services.AddSingleton<IDebateFacade>(debateFacade);

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
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("AllowAnyOrigin");

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(current => { current.SwaggerEndpoint("/swagger/v1/swagger.json", "Consensus"); });
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
    }
}