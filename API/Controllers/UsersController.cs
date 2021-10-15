using API.DTOs.Users;
using API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase
    {

        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger
            , IUserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            var users = await _service.AddNewUser(request);
            return Ok(users);
        }
    }
}
