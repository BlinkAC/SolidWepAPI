using ParkingSpot.Core.Repositories;
using ParkingSpot.Application.DTO;
using ParkingSpot.Core.Entities;
using ParkingSpot.Application.Commands;
using ParkingSpot.Core.ValueObjects;
using ParkingSpot.Application.Exceptions;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.DomainServices;

namespace ParkingSpot.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationServices _parkingReservationService;
        private static readonly List<Reservation> _reservations = new();

        //private static int Id = 1;

        public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationServices parkingReservationServices)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationServices;

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
        public async Task CreateAsync(CreateReservation command)
        {


                var (spotId, reservationId, employeeName, licensePlate, date) = command;
            //records permiten pasar el "objeto" parcialmente
            //var (reservationId, parkingSpotId, employeeName, _, date) = command; //Es valido
            //var parkingSpotId = new ParkingSpotId(spotId);

            var week = new Week(_clock.Current());
            var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
            var parkingSpotId = new ParkingSpotId(spotId);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

                if (parkingSpotToReserve == null)
                {
                    throw new WeeklyParkingSpotNotFoundException(spotId);
                }

                var reservation = new Reservation(reservationId, employeeName, licensePlate, new Date(date));

            _parkingReservationService.ReserveSpotForVehicle(
                weeklyParkingSpots,
                Jobtitle.Employee,
                parkingSpotToReserve,
                reservation);
                await _weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);

        }

        public async Task UpdateAsync(ChangeReservationLicensePlate command)
        {

                var weeklyParkingSpot = await GetParkingSpotById(command.ReservationId);


                if (weeklyParkingSpot is null)
                {
                    throw new WeeklyParkingSpotNotFoundException();
                }

                var reservationId = new ReservationId(command.ReservationId);
                var reservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);

                if (reservation is null)
                {
                throw new ReservationSpotNotFoundException(command.ReservationId);
                }

                reservation.UpdateLicensePlate(command.LicensePlate);
                await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
   
        }

        public async Task DeleteAsync(DeleteReservation command)
        {

            var existingReservation = await GetParkingSpotById(command.ReservationId);

            if (existingReservation is null)
            {
                throw new WeeklyParkingSpotNotFoundException(command.ReservationId);
            }


            existingReservation.DeleteReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UpdateAsync(existingReservation);

        }

        public async Task<WeeklyParkingSpot> GetParkingSpotById(ReservationId id)
        {
            return (await _weeklyParkingSpotRepository
                .GetAllAsync()).SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }

        private DateTime CurrentDate() => _clock.Current();
    }
}
