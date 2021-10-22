using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oiga.Events.Enums;
using Oiga.SearchService.Data;
using Oiga.SearchService.Data.Entities;
using Oiga.SearchService.v1.Requests;
using System;
using System.Threading.Tasks;

namespace Oiga.SearchService.v1.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DaprEventsController : ControllerBase
    {
        private readonly SearchServiceDbContext context;
        private readonly IMediator mediator;
        private readonly Logger<DaprEventsController> logger;

        public DaprEventsController(IMediator mediator,Logger<DaprEventsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("listen-user-events")]
        public async Task<IActionResult> PostEvent(dynamic data)
        {
            await mediator.Send(new ProcessPubsubEventRequest
            {
                Data = data
            });
            return Ok(new { success = true });
        }
    }
}
