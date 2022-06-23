using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.ValueObjects
{
    public record FullName
    {
        public string Value { get; }

        public FullName(string value)
        {
            if(string.IsNullOrEmpty(value) || value.Length is > 100 or < 12)
            {
                throw new InvalidFullNameException(value);
            }
            Value = value;

        }
        public static implicit operator string(FullName fullName) => fullName.Value;

        public static implicit operator FullName(string value) => new(value);

        public override string ToString() => Value;
    }
}
