using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordleOnlineServer.Models.Dtos;
using WordleOnlineServer.Models.MsSqlModels;
using WordleOnlineServer.Services;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly MongoService _mongoService;
        public MatchController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtService jwtService, MongoService mongoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mongoService = mongoService;
        }


        [HttpPost("GetUserLetter", Name = "GetUserLetter")]
        public async Task<IActionResult> GetUserLetter([FromBody] GetUserLetterDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            var match = await _mongoService.GetMatchByIdentifier(dto.MatchIdentifier);

            if(match.UserSender.UserName == user.UserName)
            {
                await _mongoService.JoinMatchLetter(dto, "sender");
            }
            if (match.UserReceiver.UserName == user.UserName)
            {
                await _mongoService.JoinMatchLetter(dto, "receiver");
            }

            return Ok();

        }

        [HttpPost("SwitchUserLetter", Name = "SwitchUserLetter")]
        public async Task<IActionResult> SwitchUserLetter([FromBody] GetUserLetterDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            var match = await _mongoService.GetMatchByIdentifier(dto.MatchIdentifier);

            if (match.UserSender.UserName == user.UserName)
            {
               string value =  await _mongoService.SwitchMatchLetter(dto, "sender");

                return Json(value);
            }
            if (match.UserReceiver.UserName == user.UserName)
            {
                string value = await _mongoService.SwitchMatchLetter(dto, "receiver");

                return Json(value);
            }
            else
            {
                return NotFound();
            }


        }

    }
}
