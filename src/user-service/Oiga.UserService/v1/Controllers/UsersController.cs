using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oiga.UserService.v1.Models;
using Oiga.UserService.v1.Requests;
using System.Threading.Tasks;

namespace Oiga.UserService.v1.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDataDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> PostRegister([FromBody] RegisterUserDto data)
        {
            var usrData = await mediator.Send(new RegisterUserRequest
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Username = data.Username
            });

            return Ok(usrData);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> PostRegister(string username)
        {
            var usrData = await mediator.Send(new GetUserProfileRequest
            {
                Username = username
            });

            return Ok(usrData);
        }
    }
}
