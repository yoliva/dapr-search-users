using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oiga.SearchService.Models;
using Oiga.SearchService.v1.Requests;
using System.Threading.Tasks;

namespace Oiga.SearchService.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserSearchController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserSearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Produces(typeof(PaginatedResult<UserDto>))]
        public async Task<ActionResult> GetUsersPage([FromQuery] SearchQuery search)
        {
            var response = await mediator.Send(new SearchUserRequest
            {
                ContinuationToken = search.ContinuationToken,
                Limit = search.Limit,
                SearchExpression = search.SearchExpression
            });

            return Ok(response);
        }
    }
}
