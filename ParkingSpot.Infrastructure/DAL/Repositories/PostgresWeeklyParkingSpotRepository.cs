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
        public void Add(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpot.Add(parkingSpot);
            _dbContext.SaveChanges();
        }

        public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpot.Include(x => x.Reservations).SingleOrDefault(x => x.Id == id);

        public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpot.Include(x => x.Reservations).ToList();

        public IEnumerable<WeeklyParkingSpot> GetByWeek(Week week)
        => _weeklyParkingSpot.Include(x => x.Reservations).Where(sp => sp.Week == week).ToList();

        public void Update(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpot.Update(parkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
