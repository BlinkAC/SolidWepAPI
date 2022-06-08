using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Services;

namespace ParkingSpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();
            return services;
        } 
    }
}
