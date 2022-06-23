using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;

namespace ParkingSpot.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
        private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
        public UserController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler, IQueryHandler<GetUser, UserDto> getUserHandler)
        {
            _signUpHandler = signUpHandler;
            _getUsersHandler = getUsersHandler;
            _getUserHandler = getUserHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));

        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<UserDto>> Get(Guid userId)
        {
            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId});
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() };
            await _signUpHandler.HandleAsync(command);
            return NoContent();
        }
    }
}
