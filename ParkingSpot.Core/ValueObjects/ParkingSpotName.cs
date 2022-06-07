using ParkingSpot.Core.Exceptions;

namespace ParkingSpot.Core.ValueObjects
{
    public record ParkingSpotName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidParkingSpotNameException(Value);


        public static implicit operator string(ParkingSpotName name) => name.Value;

        public static implicit operator ParkingSpotName(string value) => new(value);
    }
}
