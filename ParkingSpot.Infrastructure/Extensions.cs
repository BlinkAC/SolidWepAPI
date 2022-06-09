using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Infrastructure.DAL;
using ParkingSpot.Infrastructure.Repositories;
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
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
                .AddSingleton<IClock, Clock>();
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
