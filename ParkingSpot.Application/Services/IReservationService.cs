using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;

namespace ParkingSpot.Application.Services
{
    public interface IReservationService
    {
        Task ReserveForVehicleAsync(ReserveParkingSpotForVehicle command);
        Task ReserveForCleaningAsync(ReserveParkingForCleaning command);
        Task DeleteAsync(DeleteReservation command);
        Task<IEnumerable<ReservationDTO>> GetAllWeeklyAsync();
        Task<ReservationDTO> GetSingleAsync(Guid Id);
        Task ChangeReservationLincensePlateAsync(ChangeReservationLicensePlate command);
    }
}