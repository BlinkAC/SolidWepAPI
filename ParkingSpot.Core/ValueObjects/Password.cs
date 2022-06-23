using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParkingSpot.Core.ValueObjects
{
    public record Password
    {
        public string Value { get; }
        private const string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        private static readonly Regex regex = new Regex
        (pattern
        , RegexOptions.Compiled);


        public Password(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidPasswordException(value); 
            }

            Value = value;
        }
        public static implicit operator Password(string value) => new(value);

        public static implicit operator string(Password value) => value?.Value;

        public override string ToString() => Value;
    }
}
