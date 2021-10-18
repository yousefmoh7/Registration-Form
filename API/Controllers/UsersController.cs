using Domain.DTOs.Users;
using Infrastructre.Services.Users;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetUser(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, int id)
        {
            var user = await _service.UpdateUser(request, id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteUser(id);
            return NoContent();
        }

    }
}
