using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.ValueObjects;


namespace ParkingSpot.Core.Policies
{
    internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
    {
        private readonly IClock _clock;

        public RegularEmployeeReservationPolicy(IClock clock)
        {
            _clock = clock;
        }

        public bool CanBeApplied(Jobtitle jobtitle)
        => jobtitle == Jobtitle.Employee;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeeReservations = weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .Count(x => x.EmployeeName == employeeName);

            return totalEmployeeReservations <= 2 && _clock.Current().Hour > 4;
        }
    }
}
