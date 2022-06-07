using ParkingSpot.Core.Exceptions;

namespace ParkingSpot.Core.ValueObjects
{
    public sealed record ReservationId
    {
        public Guid Value { get; }

        public ReservationId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidParkingSpotIdException(value);
            }

            Value = value;
        }

        public static ParkingSpotId Create() => new(Guid.NewGuid());

        public static implicit operator ReservationId(Guid value) => new(value);

        public static implicit operator Guid(ReservationId date) => date.Value;


    }
}
