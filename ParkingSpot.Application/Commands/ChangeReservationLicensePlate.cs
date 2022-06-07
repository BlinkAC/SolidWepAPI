namespace ParkingSpot.Application.Commands
{
    public sealed record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate);
}
