using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Services;

namespace ParkingSpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddSingleton<IReservationService, ReservationService>();
            return services;
        } 
    }
}
