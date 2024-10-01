using Microsoft.AspNetCore.Mvc;

namespace Mates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Method()
        {
            return "Hello World!";
        }
    }
}
