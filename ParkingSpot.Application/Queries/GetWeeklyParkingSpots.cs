using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.DTO;


namespace ParkingSpot.Application.Queries
{
    public sealed class GetWeeklyParkingSpots : IQuery<IEnumerable<WeeklyParkingSpotDTO>>
    {
        public DateTime? Date { get; set; }
    }
}
