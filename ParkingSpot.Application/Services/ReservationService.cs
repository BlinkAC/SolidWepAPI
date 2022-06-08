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

        public IEnumerable<ReservationDTO> GetAllWeekly() => _weeklyParkingSpotRepository
            .GetAll()
            .SelectMany(x => x.Reservations)
            .Select(
            x => new ReservationDTO
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                LicensePlate = x.LicensePlate,
                Date = x.Date.Value.Date
            });


        public ReservationDTO GetSingle(Guid Id) => GetAllWeekly().SingleOrDefault(x => x.Id == Id);

        //public Guid? Create(Guid parkingSpotId,CreateReservation command) 
        public Guid? Create(CreateReservation command)
        {

            try
            {
                var (spotId, reservationId, employeeName, licensePlate, date) = command;
                //records permiten pasar el "objeto" parcialmente
                //var (reservationId, parkingSpotId, employeeName, _, date) = command; //Es valido
                //var parkingSpotId = new ParkingSpotId(spotId);

                var weeklyParkingSpot = _weeklyParkingSpotRepository
                    .Get(spotId);

                if (weeklyParkingSpot == null)
                {
                    return default;
                }

                var reservation = new Reservation(reservationId, employeeName, licensePlate, new Date(date));

                weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
                _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
                return reservation.Id;
            }
            catch (CustomException ex)
            {
                return default;
            }
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            try
            {
                var weeklyParkingSpot = GetParkingSpotById(command.ReservationId);


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
                _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
                return true;
            }
            catch (CustomException ex)
            {
                return false;
            }
        }

        public bool Delete(DeleteReservation command)
        {

            var existingReservation = GetParkingSpotById(command.ReservationId);

            if (existingReservation is null)
            {
                return false;
            }


            existingReservation.DeleteReservation(command.ReservationId);
            _weeklyParkingSpotRepository.Update(existingReservation);
            return true;


        }

        public WeeklyParkingSpot GetParkingSpotById(ReservationId id)
        {
            return _weeklyParkingSpotRepository
                .GetAll()
                .SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }

        private DateTime CurrentDate() => _clock.Current();
    }
}
