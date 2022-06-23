using System.Runtime.Serialization;

namespace ParkingSpot.Core.Exceptions
{
    
    internal class InvalidFullNameException : CustomException
    {
        public string fulName { get; }
        public InvalidFullNameException(string fullName) : base("Nombre con formato invalido")
        {
        }
    }
}