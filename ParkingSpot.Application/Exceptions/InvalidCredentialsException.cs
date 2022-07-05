using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
