using API.DTOs.Users;
using API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[users]")]
    public class UsersController : ControllerBase
    {

        private readonly UserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger
            , UserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            var users = await _service.AddNewAsync(request);
            return Ok(users);
        }
    }
}
