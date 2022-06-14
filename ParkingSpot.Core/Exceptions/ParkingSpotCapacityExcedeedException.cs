using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class ParkingSpotCapacityExcedeedException : CustomException
    {
        
        public ParkingSpotCapacityExcedeedException(ParkingSpotId parkingSpotId)
            : base($"Parking spot with ID: {parkingSpotId} excedeed it's reservations capacity")
        {
            
        }
    }
}
