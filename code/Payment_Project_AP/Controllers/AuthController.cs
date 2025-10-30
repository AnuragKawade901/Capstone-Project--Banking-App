using Microsoft.AspNetCore.Mvc;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Payment_Project_AP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult PostLogin([FromBody] LoginDTO usr)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid login request");
                return BadRequest("Invalid input data.");
            }

            _logger.LogInformation("Login started");

            var loginResponse = _authService.Login(usr);

            if (loginResponse != null && loginResponse.IsSuccess)
            {
                _logger.LogInformation("Login successful");
                return Ok(loginResponse);
            }

            _logger.LogWarning("Login failed");
            return Unauthorized("Invalid credentials");
        }
    }
}
