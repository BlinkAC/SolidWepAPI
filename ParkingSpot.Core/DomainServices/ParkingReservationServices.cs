using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Exceptions;
using ParkingSpot.Core.Policies;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Core.DomainServices
{
    internal class ParkingReservationServices : IParkingReservationServices
    {
        private readonly IEnumerable<IReservationPolicy> _policies;
        private readonly IClock _clock;

        public ParkingReservationServices(IEnumerable<IReservationPolicy> policies, IClock clock)
        {
            _policies = policies;
            _clock = clock;
        }

        public IClock Clock { get; }

        public void ReserveSpotForVehicle(
            IEnumerable<WeeklyParkingSpot> allParkingSpots,
            Jobtitle jobtitle,
            WeeklyParkingSpot parkingSpotToReserve,
            VehicleReservation reservation)
        {
            var parkingSpotId = parkingSpotToReserve.Id;
            var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobtitle));

            if(policy == null)
            {
                throw new NoReservationPolicyFounException(jobtitle);
            }

            if (!policy.CanReserve(allParkingSpots, reservation.EmployeeName))
            {
                throw new CannotReserveParkingSpotException(parkingSpotId);
            }

            parkingSpotToReserve.AddReservation(reservation, new Date(_clock.Current()));
        }


        public void ReserveSpotForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date)
        {
            
            foreach(var parkingSpot in allParkingSpots)
            {
                var reservationsForSameDay = parkingSpot.Reservations.Where(x => x.Date == date);
                parkingSpot.DeleteReservations(reservationsForSameDay);
                parkingSpot.AddReservation(new CleaningReservation(ReservationId.Create().Value, date), new Date(_clock.Current()));
            }
        }
    }
}
