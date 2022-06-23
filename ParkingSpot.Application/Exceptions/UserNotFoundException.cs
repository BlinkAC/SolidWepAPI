using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public class UserNotFoundException : CustomException
    {
        public Guid Id { get; }
        public UserNotFoundException(Guid id) : base($"User {id} was not found")
        {
            Id = id;
        }
    }
}
