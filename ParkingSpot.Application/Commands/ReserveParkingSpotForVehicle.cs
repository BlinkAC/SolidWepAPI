using ParkingSpot.Application.Abstractions;

namespace ParkingSpot.Application.Commands
{
    //Los commands son inmutables por naturaleza
    public sealed record ReserveParkingSpotForVehicle(
        Guid ParkingSpotId,
        Guid ReservationId,
        Guid UserId,
        string LicensePlate,
        int capacity,
        DateTime date) : ICommand;
}
