using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.ValueObjects;
using ParkingSpot.Infrastructure.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL
{
    internal sealed class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClock _clock;
        public DatabaseInitializer(IServiceProvider serviceProvider, IClock clock)
        {
            _serviceProvider = serviceProvider;
            _clock = clock;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MySpotDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);

            //agregando los lugares de estacioamiento por unica vez
            if(await dbContext.weeklyParkingSpots.AnyAsync(cancellationToken))
            {
                return;
            }

            var weeklyParkingSpots = new List<WeeklyParkingSpot> {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
            };

            await dbContext.weeklyParkingSpots.AddRangeAsync(weeklyParkingSpots, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
