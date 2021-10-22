using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oiga.SearchService.Models;
using Oiga.SearchService.v1.Requests;
using System.Threading.Tasks;

namespace Oiga.SearchService.v1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public class UserSearchController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserSearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsersPage([FromQuery] SearchQuery search)
        {
            var response = await mediator.Send(new SearchUserRequest
            {
                ContinuationToken = search.ContinuationToken,
                Limit = search.Limit,
                SearchExpression = search.Query
            });

            return Ok(response);
        }
    }
}
