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
    public sealed class ChangeReservationLicensePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
    {
        private readonly IWeeklyParkingSpotRepository _respository;
        public ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository respository)
        {
            _respository = respository;
        }


        public async Task HandleAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetParkingSpotById(command.ReservationId);


            if (weeklyParkingSpot is null)
            {
                throw new WeeklyParkingSpotNotFoundException();
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot
                                .Reservations
                                .OfType<VehicleReservation>()
                                .SingleOrDefault(x => x.Id == reservationId);

            if (reservation is null)
            {
                throw new ReservationSpotNotFoundException(command.ReservationId);
            }

            reservation.UpdateLicensePlate(command.LicensePlate);
            await _respository.UpdateAsync(weeklyParkingSpot);
        }

        public async Task<WeeklyParkingSpot> GetParkingSpotById(ReservationId id)
        {
            return (await _respository
                .GetAllAsync()).SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }
    }
}
