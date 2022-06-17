using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Exceptions;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Commands.Handler
{
    public sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
    {
        private readonly IWeeklyParkingSpotRepository _respository;
        public DeleteReservationHandler(IWeeklyParkingSpotRepository respository)
        {
            _respository = respository;
        }
        public async Task HandleAsync(DeleteReservation command)
        {
            var existingReservation = await GetParkingSpotById(command.ReservationId);

            if (existingReservation is null)
            {
                throw new WeeklyParkingSpotNotFoundException(command.ReservationId);
            }


            existingReservation.DeleteReservation(command.ReservationId);
            await _respository.UpdateAsync(existingReservation);
        }

        public async Task<WeeklyParkingSpot> GetParkingSpotById(ReservationId id)
        {
            return (await _respository
                .GetAllAsync()).SingleOrDefault(x => x.Reservations.Any(y => y.Id == id));
        }
    }
}
