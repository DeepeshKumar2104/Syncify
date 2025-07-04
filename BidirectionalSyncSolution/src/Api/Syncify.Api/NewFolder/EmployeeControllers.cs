using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syncify.Application.Models;
using Syncify.Application.Interfaces;
using System.Threading.Tasks;

namespace Syncify.Api.NewFolder
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeControllers : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeControllers(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RequestModels models)
        {
            if (models == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _userService.RegisterEmployee(models);

            if (result)
            {
                return Ok("Employee registered successfully.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering the employee.");
        }
    }
}
