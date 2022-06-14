using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    internal sealed class CannotReserveParkingSpotException : CustomException
    {
        public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId)
            : base($"Cannot reserve parking spot with ID {parkingSpotId} due to internal policies")
        {
        }
    }
}
