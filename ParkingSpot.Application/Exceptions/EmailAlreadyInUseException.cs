using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public sealed class EmailAlreadyInUseException : CustomException
    {
        public string Email { get;  }
        public EmailAlreadyInUseException(string email) : base($"Invalid email, {email} already in use")
        {
            Email = email;
        }
    }
}
