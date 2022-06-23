using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidUserNameException : CustomException
    {
        public string Username { get; }
        public InvalidUserNameException(string username) : base($"Valor de username {username} invalido")
        {
            Username = username;
        }
    }
}
