namespace ParkingSpot.Core.Exceptions
{
    public class InvalidParkingSpotNameException : CustomException
    {
        public string Name { get; }
        public InvalidParkingSpotNameException(string Name) : base(
            $"Nombre {Name} de lugar incorrecto"
            )
        {
        }
    }
}
