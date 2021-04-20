using Social.DAL.Contas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Social.Service.Interfaces;
using Social.Service.Services;
using System;

namespace Social.RestFullAPI.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<SocialContext>(options => {
                options.UseInMemoryDatabase(AppSettings.Configuration.GetConnectionString("SocialBase"));
                //options.UseSqlServer(AppSettings.Configuration.GetConnectionString("SocialBase")); // script
            });

            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<ITransferenciaRepository, TransferenciaRepository>();

            services.AddTransient<ITransacoesService, TransacoesService>();
            services.AddTransient<IContaService, ContaService>();

            services.AddControllers();

            services.AddApiVersioning();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("MEUSEGREDOSUPERSECRETO")),
                    ClockSkew = TimeSpan.FromMinutes(2),
                    ValidIssuer = "MueSistema",
                    ValidAudience = "https://localhost",
                };
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins("http://localhost"));

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
