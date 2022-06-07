namespace ParkingSpot.Application.Commands
{
    //Los commands son inmutables por naturaleza
    public sealed record CreateReservation(
        Guid ParkingSpotId,
        Guid ReservationId,
        string EmployeeName,
        string LicensePlate,
        DateTime date);
}
