using Microsoft.AspNetCore.Mvc;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
      
        [HttpGet(Name = "First")]
        public IActionResult Get()
        {
            Console.WriteLine("ok");
            return Ok();
            
        }

    }
}
