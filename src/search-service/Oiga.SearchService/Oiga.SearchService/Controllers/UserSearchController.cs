using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Oiga.SearchService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserSearchController : ControllerBase
    {
        private readonly ILogger<UserSearchController> _logger;

        public UserSearchController(ILogger<UserSearchController> logger)
        {
            _logger = logger;
        }
    }
}
