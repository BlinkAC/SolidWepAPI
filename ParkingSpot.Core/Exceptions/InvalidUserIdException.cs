using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidUserIdException : CustomException
    {
        public Guid userId { get; }
        public InvalidUserIdException(Guid userId) : base($"Valor {userId} incorrecto")
        {
        }
    }
}
