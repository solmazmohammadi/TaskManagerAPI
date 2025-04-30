using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello, .NET!";
        }
    }
}