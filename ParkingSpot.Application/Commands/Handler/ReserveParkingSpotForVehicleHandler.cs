using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Exceptions;
using ParkingSpot.Core.DomainServices;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Commands.Handler
{
    public sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
    {

        private readonly IWeeklyParkingSpotRepository _respository;
        private readonly IParkingReservationServices _domainSerives;
        private readonly IUserRepository _userRepository;
        private readonly IClock _clock;
        public ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository respository, IParkingReservationServices domainSerives, IClock clock, IUserRepository userRepository)
        {
            _respository = respository;
            _domainSerives = domainSerives;
            _clock = clock;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(ReserveParkingSpotForVehicle command)
        {


            var (spotId, reservationId, userId, licencePlate, capacity, date) = command;

            var week = new Week(_clock.Current());
            
            var weeklyParkingSpots = await _respository.GetByWeekAsync(week);
            var parkingSpotId = new ParkingSpotId(spotId);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (parkingSpotToReserve == null)
            {
                throw new WeeklyParkingSpotNotFoundException(spotId);
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var reservation = new VehicleReservation(
                reservationId,
                new Date(date),
                capacity,
                new EmployeeName(user.FullName),
                licencePlate, user.Id);

            _domainSerives.ReserveSpotForVehicle(
                weeklyParkingSpots,
                Jobtitle.Employee,
                parkingSpotToReserve,
                reservation);

            await _respository.UpdateAsync(parkingSpotToReserve);
        }
    }
}
