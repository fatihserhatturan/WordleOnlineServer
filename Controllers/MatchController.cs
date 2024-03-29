using Microsoft.AspNetCore.Mvc;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
