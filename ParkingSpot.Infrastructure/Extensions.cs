using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.Services;
using ParkingSpot.Infrastructure.DAL;
using ParkingSpot.Infrastructure.Exceptions;
using ParkingSpot.Infrastructure.Logging;
using ParkingSpot.Infrastructure.Repositories;
using ParkingSpot.Infrastructure.Security;
using ParkingSpot.Infrastructure.Time;
using System.Runtime.CompilerServices;

//Hace que sea visible para este proeycto
//En este caso esta bien porque es para efectos de pruebas.
[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
namespace ParkingSpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            

            services
                .AddPostgres(configuration)
                .AddSingleton<ExceptionMiddleware>()
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
                .AddSingleton<IClock, Clock>();

            services.AddCustomLogging();
            services.AddSecurity();

            var applicationAssembly = typeof(AppOptions).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
