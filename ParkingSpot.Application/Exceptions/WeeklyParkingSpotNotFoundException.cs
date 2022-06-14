using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public sealed class WeeklyParkingSpotNotFoundException : CustomException
    {
        public Guid? Id { get; }
        public WeeklyParkingSpotNotFoundException()
            : base($"weekly parking spot with id was not found")
        {
            
        }

        public WeeklyParkingSpotNotFoundException(Guid id)
            : base($"weekly parking spot with id {id} was not found")
        {
            Id = id;
        }

    }
}
