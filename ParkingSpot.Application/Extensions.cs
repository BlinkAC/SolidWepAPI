using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.Commands.Handler;
using ParkingSpot.Application.Services;

namespace ParkingSpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();

            //ParaEvitar llenar con todos los commands se usa scrutor
            //services.AddScoped<ICommandHandler<ReserveParkingSpotForVehicle>, ReserveParkingSpotForVehicleHandler>();

            var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        } 
    }
}
