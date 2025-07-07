using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get(IConfiguration config)
        {
            return Ok(new
            {
                environment = config.GetValue<string>("env")
            });
        }
            

    }
}
