using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using UpsideDownKitten.BL.Clients;
using UpsideDownKitten.BL.Services;
using UpsideDownKitten.Common;
using UpsideDownKitten.DL;

namespace UpsideDownKitten
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ICatsService, CatsService>();
            services.AddTransient<ICatsClient, CatsClient>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UpsideDown Kitten API",
                    Description = "Simple project developed for rotating cat images"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


                // AddBasic(c);
                AddBarier(c);

            });


            AddAuthServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpsideDown Kitten API V1");
            });

            app.UseAuthentication();
            app.UseMvc();
        }


        private static void AddBasic(SwaggerGenOptions c)
        {
            c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Description = "Basic auth added to authorization header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "basic",
                Type = SecuritySchemeType.Http,

            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Basic"}
                    },
                    new List<string>()
                }
            });
        }

        private static void AddBarier(SwaggerGenOptions c)
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field"
            });

            c.OperationFilter<AddAuthHeaderOperationFilter>();
        }

        private static void AddAuthServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
        }
    }

    public class AddAuthHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isBasicAuth = actionMetadata.Any(x => x is BasicAuthAttribute);
            if (!isBasicAuth)
            {
                return;
            }
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Security = new List<OpenApiSecurityRequirement>();

            //Add JWT bearer type
            operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                // Definition name. 
                                // Should exactly match the one given in the service configuration
                                Id = "Bearer"
                            }
                        }, new string[0]
                    }
                }
            );
        }
    }
}
