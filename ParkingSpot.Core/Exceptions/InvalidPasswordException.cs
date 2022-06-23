using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    internal class InvalidPasswordException : CustomException
    {
        public string Password { get; set; }
        public InvalidPasswordException(string password) : 
            base($"Invalid password {password} format/nThe format is: minimum 8 characters/nAt least one uppercase/nAt least one lowercase/nAt least one digit/nAt least one special character")
        {
               Password = password;
        }
    }
}
