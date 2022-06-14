using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.ValueObjects
{
    public sealed record Jobtitle
    {
        public string Value { get; }

        public const string Employee = nameof(Employee);
        public const string Manager = nameof(Manager);
        public const string Boss = nameof(Boss);

        private Jobtitle (string value) => Value = value;

        public static implicit operator string(Jobtitle jobtitle)
        => jobtitle.Value;

        public static implicit operator Jobtitle(string value)
            => new(value);
    }
}
