using ParkingSpot.Application.Abstractions;

namespace ParkingSpot.Application.Commands
{
    public sealed record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate) : ICommand;
}
