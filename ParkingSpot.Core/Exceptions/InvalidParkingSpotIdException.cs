namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidParkingSpotIdException : CustomException
    {
        public Guid parkinSpotId { get; }
        public InvalidParkingSpotIdException(Guid parkinSpotId) :
            base($"Valor {parkinSpotId} incorrecto")
        {
        }
    }
}
