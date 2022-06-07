using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;

namespace ParkingSpot.Application.Services
{
    public interface IReservationService
    {
        Guid? Create(CreateReservation command);
        bool Delete(DeleteReservation command);
        IEnumerable<ReservationDTO> GetAllWeekly();
        ReservationDTO GetSingle(Guid Id);
        bool Update(ChangeReservationLicensePlate command);
    }
}