namespace ParkingSpot.Core.Exceptions
{
    public sealed class invalidLicensePlateException : CustomException
    {
        public string LicensePlate { get; set; }
        public invalidLicensePlateException(string licensePlate) :
            base($"Error, placa de auto {licensePlate} es invalida")
        {
            LicensePlate = licensePlate;
        }
    }
}
