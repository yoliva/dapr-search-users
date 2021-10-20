using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Oiga.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
    }
}
