using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            // Return a simple JSON object
            return Ok(new { message = "Hello from your .NET 8 Backend!" });
        }
    }
}
