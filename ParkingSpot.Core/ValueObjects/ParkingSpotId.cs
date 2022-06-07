using ParkingSpot.Core.Exceptions;

namespace ParkingSpot.Core.ValueObjects
{
    public sealed record ParkingSpotId
    {
        public Guid Value { get; }

        public ParkingSpotId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidParkingSpotIdException(value);
            }

            Value = value;
        }

        public static ParkingSpotId Create() => new(Guid.NewGuid());

        public static implicit operator ParkingSpotId(Guid value) => new(value);

        public static implicit operator Guid(ParkingSpotId date) => date.Value;

        public override string ToString() => Value.ToString("N");


    }
}
