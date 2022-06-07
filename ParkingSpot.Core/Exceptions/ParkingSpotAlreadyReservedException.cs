namespace ParkingSpot.Core.Exceptions
{
    public sealed class ParkingSpotAlreadyReservedException : CustomException
    {
        public string SpotName { get; }

        public DateTime Date { get; set; }

        public ParkingSpotAlreadyReservedException(string spotName, DateTime date) :
            base($"Lugar {spotName} ya ha sido reservado para{date}")
        {
            SpotName = spotName;
            Date = date;
        }
    }
}
