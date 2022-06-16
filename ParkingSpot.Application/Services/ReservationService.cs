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
                EmployeeName = x is VehicleReservation vr? vr.EmployeeName : null,
                Date = x.Date.Value.Date
            });


        public async Task<ReservationDTO> GetSingleAsync(Guid Id) => (await GetAllWeeklyAsync()).SingleOrDefault(x => x.Id == Id);

        //public Guid? Create(Guid parkingSpotId,CreateReservation command) 
        public async Task ReserveForVehicleAsync(ReserveParkingSpotForVehicle command)
        {


        }

        public async Task ReserveForCleaningAsync(ReserveParkingForCleaning command)
        {


        }

        public async Task ChangeReservationLincensePlateAsync(ChangeReservationLicensePlate command)
        {


        }

        public async Task DeleteAsync(DeleteReservation command)
        {


        }

        public async Task<WeeklyParkingSpot> GetParkingSpotById(ReservationId id)
        {
            return (await _weeklyParkingSpotRepository
                .GetAllAsync()).SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }

        private DateTime CurrentDate() => _clock.Current();


    }
}
