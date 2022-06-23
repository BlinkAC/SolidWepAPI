using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidEntityIdException : CustomException
    {
        public object Id { get; }

        public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
            => Id = id;
    }
}
