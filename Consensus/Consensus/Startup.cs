using System;
using System.IO;
using System.Net;
using System.Reflection;
using Consensus.Filters;
using Consensus.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ConsensusLibrary.UserContext;
using ConsensusLibrary.CryptoContext;

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

            services.AddSingleton<IRegistrationFacade>(registrationFacade);

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

            var optionModel = new OptionModel()
            {
                Secret = Configuration.GetValue<string>("Secret"),
                OpenviduUrl = Configuration.GetValue<string>("OpenviduUrl")
            };

            services.AddSingleton(optionModel);

            services.AddMvc(o => 
            {
                o.Filters.Add(new ExceptionFilter());
            })
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAnyOrigin");

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(current => { current.SwaggerEndpoint("/swagger/v1/swagger.json", "Consensus"); });
        }
    }
}
