using ParkingSpot.Application.Abstractions;
using ParkingSpot.Core.DomainServices;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.ValueObjects;


namespace ParkingSpot.Application.Commands.Handler
{
    public sealed class ReserveParkingForCleaningHandler : ICommandHandler<ReserveParkingForCleaning>
    {
        private readonly IWeeklyParkingSpotRepository _repository;
        private readonly IParkingReservationServices _reservationService;

        public ReserveParkingForCleaningHandler(IWeeklyParkingSpotRepository repository, IParkingReservationServices reservationService, IUserRepository userRepository)
        {
            _repository = repository;
            _reservationService = reservationService;

        }

        public async Task HandleAsync(ReserveParkingForCleaning command)
        {
            var week = new Week(command.date);
            var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();


            _reservationService.ReserveSpotForCleaning(weeklyParkingSpots, new Date(command.date));

            //provisional-- sin esto arroja excepcion de concurrencia
            //foreach (var parkingSpot in weeklyParkingSpots)
            //{
            //    await _repository.UpdateAsync(parkingSpot);
            //}
            var tasks = weeklyParkingSpots.Select(x => _repository.UpdateAsync(x));
            await Task.WhenAll(tasks);
        }
    }
}
