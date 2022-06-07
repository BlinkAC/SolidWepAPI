namespace ParkingSpot.Core.Exceptions
{
    public class InvalidEmployeeNameException : CustomException
    {
        public InvalidEmployeeNameException() :
            base($"Nombre con formato invalido")
        {
        }
    }
}
