using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Infrastructure.Logging.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.Logging
{
    public static class Extensions 
    {
        public static IServiceCollection AddCustomLogging(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            return services;
        }
    }
}
