using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordleOnlineServer.Models.Dtos;
using WordleOnlineServer.Models.MsSqlModels;
using WordleOnlineServer.Services;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly MongoService _mongoService;
        public LobbyController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtService jwtService, MongoService mongoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mongoService = mongoService;
        }

        [HttpPost("GetInLetterLobby", Name = "GetInLetterLobby")]
        public async Task<IActionResult> GetInLobby([FromBody]LobbyTransferDTO dTO)
        {
            var user = await _userManager.FindByNameAsync(dTO.UserName);

            if (user == null)
                return NotFound();

            await _mongoService.AttendLetterLobby(user, dTO.LetterCount);


            return Ok("İşlem Başarılı");
        }

        [HttpPost("GetOutLetterLobby", Name = "GetOutLetterLobby")]
        public async Task<IActionResult> GetOutFourLetterLobby([FromBody] LobbyTransferDTO dTO)
        {
            var user = await _userManager.FindByNameAsync(dTO.UserName);

            if (user == null)
            {
                return NotFound();
            }

            await _mongoService.LeaveLetterLobby(user);

            return Ok("İşlem Başarılı");
        }
    }
}
