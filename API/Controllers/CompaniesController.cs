using API.DTOs.Compaines;
using API.Services.Compaines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/companies")]

    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyService _service;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ILogger<CompaniesController> logger
            , ICompanyService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCompanyRequest request)
        {
            var users = await _service.AddNewCompany(request);
            return Ok(users);
        }
    }
}
