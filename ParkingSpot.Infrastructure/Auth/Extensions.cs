using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ParkingSpot.Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.Auth
{
    internal static class Extensions
    {

        private const string OptionsSectionName = "auth";

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<AuthOptions>(OptionsSectionName);

            services
                .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
                .AddSingleton<IAuthenticator, Authenticator>()
                .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
                .AddAuthentication(o =>
                {
                    //agrega bearer como header en los request
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Audience = options.Audience;
                    o.IncludeErrorDetails = true; //no deberia estar en produccion
                    o.TokenValidationParameters = new TokenValidationParameters
                    {//con estas propiedades el servidor sabe como validar el token
                        ValidIssuer = options.Issuer,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                    };
                });

            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("is-admin", policy =>
                {
                    policy.RequireRole("admin");
                });
            });


            return services;
        }
    }
}
