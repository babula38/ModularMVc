using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Module.One
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] ITestService testService) => Ok(testService.Message());

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{i}")]
        public IActionResult Get([FromServices] ITestService testService, int i)
        {
            if (i == 9)
                return Ok("ddd");
            else
                return NotFound();
        }
    }

    public class TestService : ITestService
    {
        public string Message() => "From module one via service";

    }
}