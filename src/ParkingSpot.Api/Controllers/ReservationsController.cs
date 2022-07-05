using Microsoft.AspNetCore.Mvc;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Services;

namespace ParkingSpot.Api.Controllers
{
    [Route("parking-spots")]
    public class ReservationsController : ControllerBase
    {
        private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotsForVehicleHandler;
        private readonly ICommandHandler<ReserveParkingForCleaning> _reserveParkingSpotsForCleaningHandler;
        private readonly ICommandHandler<ChangeReservationLicensePlate> _changeReservationLicencePlateHandler;
        private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;

        public ReservationsController(
        ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotsForVehicleHandler,
        ICommandHandler<ReserveParkingForCleaning> reserveParkingSpotsForCleaningHandler,
        ICommandHandler<ChangeReservationLicensePlate> changeReservationLicencePlateHandler,
        ICommandHandler<DeleteReservation> deleteReservationHandler)
        {
            _reserveParkingSpotsForVehicleHandler = reserveParkingSpotsForVehicleHandler;
            _reserveParkingSpotsForCleaningHandler = reserveParkingSpotsForCleaningHandler;
            _changeReservationLicencePlateHandler = changeReservationLicencePlateHandler;
            _deleteReservationHandler = deleteReservationHandler;
        }

        [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
        public async Task<ActionResult> post(Guid parkingSpotId,[FromBody] ReserveParkingSpotForVehicle command)
        {
            var prueba = User.Identity.Name;
            await _reserveParkingSpotsForVehicleHandler.HandleAsync(command with {
                    ReservationId = Guid.NewGuid(),
                    ParkingSpotId = parkingSpotId,
                    UserId = Guid.Parse(User.Identity.Name)
            });

            return NoContent();
        }

        [HttpPost("reservations/cleaning")]
        public async Task<ActionResult> post([FromBody] ReserveParkingForCleaning command)
        {
            await _reserveParkingSpotsForCleaningHandler.HandleAsync(command);
            return NoContent();
        }


        [HttpPut("reservations/{reservationId:guid}")]
        public async Task<ActionResult> put(Guid reservationId, [FromBody] ChangeReservationLicensePlate command)
        {
            
            await _changeReservationLicencePlateHandler.HandleAsync(command with { ReservationId = reservationId } );


            return NoContent();
        }

        [HttpDelete("reservations/{reservationId:guid}")]
        public async Task<ActionResult> delete( Guid reservationId)
        {
            await _deleteReservationHandler.HandleAsync( new DeleteReservation(reservationId) );    

            return NoContent();
        }
    }
}
