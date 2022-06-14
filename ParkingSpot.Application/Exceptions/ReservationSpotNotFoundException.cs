using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public sealed class ReservationSpotNotFoundException : CustomException
    {
        public Guid Id { get; }
        public ReservationSpotNotFoundException(Guid id) : base($"reservation with id {id} was not found")
        {
            Id = id;
        }
    }
}
