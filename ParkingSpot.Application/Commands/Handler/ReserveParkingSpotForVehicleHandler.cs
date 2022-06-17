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
        private readonly IClock _clock;
        public ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository respository, IParkingReservationServices domainSerives, IClock clock)
        {
            _respository = respository;
            _domainSerives = domainSerives;
            _clock = clock;
        }

        public async Task HandleAsync(ReserveParkingSpotForVehicle command)
        {


            var (spotId, reservationId, employeeName, licensePlate, capacity, date) = command;

            var week = new Week(_clock.Current());
            
            var weeklyParkingSpots = await _respository.GetByWeekAsync(week);
            var parkingSpotId = new ParkingSpotId(spotId);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (parkingSpotToReserve == null)
            {
                throw new WeeklyParkingSpotNotFoundException(spotId);
            }

            var reservation = new VehicleReservation(reservationId, new Date(date), capacity, employeeName, licensePlate);

            _domainSerives.ReserveSpotForVehicle(
                weeklyParkingSpots,
                Jobtitle.Employee,
                parkingSpotToReserve,
                reservation);

            await _respository.UpdateAsync(parkingSpotToReserve);
        }
    }
}
