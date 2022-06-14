using ParkingSpot.Core.Entities;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Policies
{
    internal sealed class ManagerReservationPolicy : IReservationPolicy
    {
        
            public bool CanBeApplied(Jobtitle jobtitle)
            => jobtitle == Jobtitle.Manager;
        

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(x => x.EmployeeName == employeeName);

            return totalEmployeeReservations <= 4;
        }
    }
}
