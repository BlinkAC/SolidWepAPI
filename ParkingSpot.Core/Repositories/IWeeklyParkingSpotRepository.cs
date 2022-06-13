using ParkingSpot.Core.Entities;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
        Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
        Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week);
        Task AddAsync(WeeklyParkingSpot parkingSpot);
        Task UpdateAsync(WeeklyParkingSpot parkingSpot);

    }
}
