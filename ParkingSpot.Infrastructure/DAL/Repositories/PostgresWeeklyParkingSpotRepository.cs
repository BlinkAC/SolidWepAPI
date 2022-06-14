using Microsoft.EntityFrameworkCore;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly MySpotDbContext _dbContext;
        private readonly DbSet<WeeklyParkingSpot> _weeklyParkingSpot;

        public PostgresWeeklyParkingSpotRepository(MySpotDbContext context)
        {
            _dbContext = context;
            _weeklyParkingSpot = _dbContext.weeklyParkingSpots;
        }
        public async Task AddAsync(WeeklyParkingSpot parkingSpot)
        {
            await _weeklyParkingSpot.AddAsync(parkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id) => 
            await _weeklyParkingSpot
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => 
            await _weeklyParkingSpot.Include(x => x.Reservations)
            .ToListAsync();

        //Se supone
        public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        {
            var spots =await _weeklyParkingSpot.Include(x => x.Reservations)
                .ToListAsync();

                         
            return spots;
        }

        public async Task UpdateAsync(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpot.Update(parkingSpot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
