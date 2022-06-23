using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.ValueObjects
{
    public record Username
    {
        public string Value { get; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is > 20 or < 8)
            {
                throw new InvalidUserNameException(value);
            }
            Value = value;
        }

        public static implicit operator string(Username userName) => userName.Value;

        public static implicit operator Username(string value) => new(value);

        public override string ToString() => Value;
    }
}
