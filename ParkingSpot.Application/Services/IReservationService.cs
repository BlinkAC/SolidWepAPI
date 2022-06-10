using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;

namespace ParkingSpot.Application.Services
{
    public interface IReservationService
    {
        Task<Guid?> CreateAsync(CreateReservation command);
        Task<bool> DeleteAsync(DeleteReservation command);
        Task<IEnumerable<ReservationDTO>> GetAllWeeklyAsync();
        Task<ReservationDTO> GetSingleAsync(Guid Id);
        Task<bool> UpdateAsync(ChangeReservationLicensePlate command);
    }
}