using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;
using System.Reflection;

namespace Social.RestFullAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "Social Transações API",
                    Description = "Gerenciamento de contas e pagamentos",
                    Contact = new OpenApiContact() { Name = "Bruno Daldegan", Email = "bcdaldegan@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") },
                    Version = "v1.0"
                });

                c.SwaggerDoc("v2.0", new OpenApiInfo
                {
                    Title = "Social Transações API",
                    Description = "Gerenciamento de contas e pagamentos",
                    Contact = new OpenApiContact() { Name = "Bruno Daldegan", Email = "bcdaldegan@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") },
                    Version = "v2.0"
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Autorização usando Bearer Scheme. Exemplo: \"Authorization: Bearer {token}\"",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";

                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1.0/swagger.json", "Version 1.0");
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v2.0/swagger.json", "Version 2.0");
                c.DocumentTitle = "API Restfull para gerenciamento de contas e pagamentos";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
