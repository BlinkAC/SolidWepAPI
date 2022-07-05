using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;
using ParkingSpot.Application.Security;

namespace ParkingSpot.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
        private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
        private readonly ICommandHandler<SignIn> _signInHandler;
        private readonly ITokenStorage _tokenStorage;
        public UserController(ICommandHandler<SignUp> signUpHandler,
            ICommandHandler<SignIn> signInHandler,
            IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
            IQueryHandler<GetUser, UserDto> getUserHandler,
            ITokenStorage tokenStorage)
        {
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
            _getUsersHandler = getUsersHandler;
            _getUserHandler = getUserHandler;
            _tokenStorage = tokenStorage;
        }

        [Authorize(Policy = "is-admin")]
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

        [HttpPost("sign-in")]
        public async Task<JwtDto> Post(SignIn command)
        {
            await _signInHandler.HandleAsync(command);
            var jwt = _tokenStorage.Get();
            return jwt;
        }

        //[Authorize(Policy = "is-admin")]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Get()
        {
            //if (string.IsNullOrEmpty(User.Identity?.Name))
            //{
            //    return NotFound();
            //}

            //var isInAdminRole = User.IsInRole("admin");
            //var isInUserRole = User.IsInRole("user");
            var userId = Guid.Parse(User.Identity?.Name);

            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
            return Ok(user);
        }

        //Es una poliza que debe ser aplicada para el usuario autenticado
        //no tiene que ser necesariamente el rol
        //puede ser cualquier tipo de check para dejar al usuario autenticado ejecutar la accion
        [Authorize(Policy = "is-admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
            => Ok(await _getUsersHandler.HandleAsync(query));

        //[HttpGet("jwt")]
        //[AllowAnonymous]
        //public ActionResult<JwtDto> GetToken()
        //{

        //    var jwt = _authenticator.CreateToken(Guid.NewGuid(), "Boss");
        //    return jwt;
        //}
    }
}
