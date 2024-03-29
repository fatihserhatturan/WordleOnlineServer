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

        [HttpPost("GetLobbyMember", Name = "GetLobbyMember")]
        public async Task<IActionResult> GetlobbyMember([FromBody] string lobby)
        {

            if (int.Parse(lobby) == 4)
                return Json(_mongoService.GetFourLobbyMember());

            if(int.Parse(lobby) == 5)
                return Json(_mongoService.GetFiveLobbyMember());

            if(int.Parse(lobby) == 6)
                return Json(_mongoService.GetSixLobbyMember());

            if(int.Parse(lobby) == 7)
                return Json(_mongoService.GetSevenLobbyMember());
            else
                return NotFound();
            
        }

        [HttpPost("SendMatchRequest", Name = "SendMatchRequest")]
        public async Task<IActionResult> SendMatchRequest([FromBody] SendMatchRequestDTO request)
        {
            var sender = await _userManager.FindByNameAsync(request.Sender);
            var receiver =await _userManager.FindByNameAsync(request.Receiver);

            await _mongoService.SendMatchRequest(sender,receiver);

            return Ok();
        }

        [HttpPost("ReceiveMatchRequest", Name = "ReceiveMatchRequest")]
        public async Task<IActionResult> ReceiveMatchRequest([FromBody] string userName)
        {
            var receiver = await _userManager.FindByNameAsync(userName);

            var result = await _mongoService.ReceiveMatchRequest(receiver);

            if(result == null || receiver == null)
                return NotFound();
            if(result!=null)
                return Json(result);

            return BadRequest();
        }

        [HttpPost("AcceptMatchRequest", Name = "AcceptMatchRequest")]
        public async Task<IActionResult> AcceptMatchRequest([FromBody] string userName)
        {
            var receiver = await _userManager.FindByNameAsync(userName);

            var result = await _mongoService.ReceiveMatchRequest(receiver);

            if (result == null || receiver == null)
                return NotFound();
            if (result != null)
                return Json(result);

            return BadRequest();
        }
    }
}
