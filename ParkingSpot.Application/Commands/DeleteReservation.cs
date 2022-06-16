using ParkingSpot.Application.Abstractions;

namespace ParkingSpot.Application.Commands
{
    public sealed record DeleteReservation(Guid ReservationId) : ICommand;
}
