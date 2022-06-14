using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;

namespace ParkingSpot.Application.Services
{
    public interface IReservationService
    {
        Task CreateAsync(CreateReservation command);
        Task DeleteAsync(DeleteReservation command);
        Task<IEnumerable<ReservationDTO>> GetAllWeeklyAsync();
        Task<ReservationDTO> GetSingleAsync(Guid Id);
        Task UpdateAsync(ChangeReservationLicensePlate command);
    }
}