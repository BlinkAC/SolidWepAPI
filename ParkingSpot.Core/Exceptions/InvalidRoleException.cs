using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidRoleException : CustomException
    {
        public string Role { get; }
        public InvalidRoleException(string role) : base($"The given {role} role is invalid")
        {
            Role = role;
        }
    }
}
