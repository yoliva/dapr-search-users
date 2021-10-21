using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oiga.UserService.v1.Models;
using Oiga.UserService.v1.Requests;
using System.Threading.Tasks;

namespace Oiga.UserService.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Produces(typeof(UserDataDto))]
        public async Task<ActionResult> PostRegister([FromBody] RegisterUserDto data)
        {
            var usrData = await mediator.Send(new RegisterUserRequest
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Username = data.UserName
            });

            return Ok(usrData);
        }
    }
}
