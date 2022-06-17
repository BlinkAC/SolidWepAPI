using Microsoft.AspNetCore.Mvc;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;

namespace ParkingSpot.Api.Controllers
{
    [Route("parking-spots")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDTO>>
        _getWeeklyParkingSpotsHandler;

        public ParkingSpotController(IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDTO>> getWeeklyParkingSpotsHandler)
        {
            _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDTO>>> Get([FromQuery] GetWeeklyParkingSpots query)
        {
            return Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(query));
        }
    }
}
