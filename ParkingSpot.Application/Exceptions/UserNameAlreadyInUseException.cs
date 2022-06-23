using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Exceptions
{
    public sealed class UserNameAlreadyInUseException : CustomException
    {
        public string Username { get; set; }
        public UserNameAlreadyInUseException(string username) : base($"Invalid given username, the {username} is already taken")
        {
            Username = username;
        }
    }
}
