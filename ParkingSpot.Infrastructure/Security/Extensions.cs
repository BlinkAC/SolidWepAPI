using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Secutiry;
using ParkingSpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.Security
{
    internal static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services) 
        { 
            services
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddSingleton<IPasswordManager, PasswordManager>();
            return services;
        }
    }
}
