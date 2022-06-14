using ParkingSpot.Core.Entities;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.DomainServices
{
    public interface IParkingReservationServices
    {
        public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, Jobtitle jobtitle, 
            WeeklyParkingSpot weeklyParkingSpotToReserve, VehicleReservation reservation);

        void ReserveSpotForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date);

    }
}
