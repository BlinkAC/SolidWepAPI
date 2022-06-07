using Microsoft.AspNetCore.Mvc;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Services;

namespace ParkingSpot.Api.Controllers
{
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        //private readonly ReservationService _service = new( new Clock(), new List<WeeklyParkingSpot>
        //{
        //    new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(new Clock().Current()), "P1"),
        //    new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(new Clock().Current()), "P2"),
        //    new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(new Clock().Current()), "P3"),
        //    new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(new Clock().Current()), "P4"),
        //    new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(new Clock().Current()), "P5")
        //} );

        //private static int Id = 1;

        private readonly IReservationService _reservationService;

        public ReservationsController(IClock clock, IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet]
        public ActionResult<ReservationDTO[]> Get() => Ok(_reservationService.GetAllWeekly());


        [HttpGet("{Id:guid}")]
        public ActionResult<ReservationDTO> GetSingle( Guid Id)
        {
            var reservation = _reservationService.GetSingle(Id);

            if (reservation is null)
            {
                return NotFound();
            }
            return reservation;
        }

        [HttpPost]
        public ActionResult post([FromBody] CreateReservation command)
        {
            var id = _reservationService.Create(command with { ReservationId = Guid.NewGuid() } );

            if(id == null)
            {
                return BadRequest();
            }

            //Url hardcodeada
            //return Created($"reservartions/{reservation.Id}", default);

            //Al cambiar el metodo cambia la url
            //nameof hace referencia al metodo
            return CreatedAtAction(nameof(GetSingle), new { Id = id }, default);
        }

        [HttpPut("{Id:guid}")]
        public ActionResult put( Guid Id, ChangeReservationLicensePlate command)
        {
            
            var succeeded = _reservationService.Update(command with { ReservationId = Id } );

            if(!succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public ActionResult delete( Guid Id)
        {
            var succeeded = _reservationService.Delete( new DeleteReservation(Id) );    

            if (!succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
