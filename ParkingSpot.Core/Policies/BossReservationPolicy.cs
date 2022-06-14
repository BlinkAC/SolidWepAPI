using ParkingSpot.Core.Entities;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Policies
{
    internal sealed class BossReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(Jobtitle jobtitle)
        => jobtitle == Jobtitle.Boss;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        => true;
    }
}
