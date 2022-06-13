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
        IEnumerable<WeeklyParkingSpot> GetAll();
        WeeklyParkingSpot Get(ParkingSpotId id);
        IEnumerable<WeeklyParkingSpot> GetByWeek(Week week);
        public void Add(WeeklyParkingSpot parkingSpot);
        public void Update(WeeklyParkingSpot parkingSpot);

    }
}
