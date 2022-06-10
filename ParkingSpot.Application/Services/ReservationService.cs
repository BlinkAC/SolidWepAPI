using ParkingSpot.Core.Repositories;
using ParkingSpot.Application.DTO;
using ParkingSpot.Core.Entities;
using ParkingSpot.Application.Commands;
using ParkingSpot.Core.Exceptions;
using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private static readonly List<Reservation> _reservations = new();

        //private static int Id = 1;

        public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;

        }

        //SOLID-------------- S Esta clase se encarga de la logica () y le deja al controlador  la 
        //unica responsabilidad de manejar los metodos y respuestas Http

        public async Task<IEnumerable<ReservationDTO>> GetAllWeeklyAsync() => (await
            _weeklyParkingSpotRepository
            .GetAllAsync())
            .SelectMany(x => x.Reservations)
            .Select(
            x => new ReservationDTO
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                LicensePlate = x.LicensePlate,
                Date = x.Date.Value.Date
            });


        public async Task<ReservationDTO> GetSingleAsync(Guid Id) => (await GetAllWeeklyAsync()).SingleOrDefault(x => x.Id == Id);

        //public Guid? Create(Guid parkingSpotId,CreateReservation command) 
        public async Task<Guid?> CreateAsync(CreateReservation command)
        {

            try
            {
                var (spotId, reservationId, employeeName, licensePlate, date) = command;
                //records permiten pasar el "objeto" parcialmente
                //var (reservationId, parkingSpotId, employeeName, _, date) = command; //Es valido
                //var parkingSpotId = new ParkingSpotId(spotId);

                var weeklyParkingSpot = await _weeklyParkingSpotRepository
                    .GetAsync(spotId);

                if (weeklyParkingSpot == null)
                {
                    return default;
                }

                var reservation = new Reservation(reservationId, employeeName, licensePlate, new Date(date));

                weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
                await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
                return reservation.Id;
            }
            catch (CustomException ex)
            {
                return default;
            }
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            try
            {
                var weeklyParkingSpot = await GetParkingSpotById(command.ReservationId);


                if (weeklyParkingSpot is null)
                {
                    return false;
                }

                var reservationId = new ReservationId(command.ReservationId);
                var reservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);

                if (reservation is null)
                {
                    return false;
                }

                reservation.UpdateLicensePlate(command.LicensePlate);
                await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
                return true;
            }
            catch (CustomException ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {

            var existingReservation = await GetParkingSpotById(command.ReservationId);

            if (existingReservation is null)
            {
                return false;
            }


            existingReservation.DeleteReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UpdateAsync(existingReservation);
            return true;


        }

        public async Task<WeeklyParkingSpot> GetParkingSpotById(ReservationId id)
        {
            return (await _weeklyParkingSpotRepository
                .GetAllAsync()).SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }

        private DateTime CurrentDate() => _clock.Current();
    }
}
