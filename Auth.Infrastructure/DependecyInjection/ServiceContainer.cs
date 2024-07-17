using Auth.Application.Contracts;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Auth.Infrastructure.DependecyInjection
{
    public  static class ServiceContainer
    {

        public static IServiceCollection InfrastructureService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"),
            b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
            ServiceLifetime.Scoped);

            services.AddAuthentication(options =>

            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }

            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true ,
                    ValidIssuer = configuration["Jwt : Issuer"],
                    ValidAudience = configuration["Jwt :Audience"],
                    IssuerSigningKey= new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:key"]!) )
                }; 
            });
             services.AddScoped<IUser, UserRepository>();
            return services;

        }
        
    }
}
