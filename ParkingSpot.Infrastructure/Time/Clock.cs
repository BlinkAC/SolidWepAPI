using ParkingSpot.Core.Services;

namespace ParkingSpot.Infrastructure.Time
{
    internal sealed class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}
