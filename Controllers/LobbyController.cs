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
        private readonly MongoService _mongoService;
        public LobbyController(UserManager<AppUser> userManager, MongoService mongoService)
        {
            _userManager = userManager;
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

            await _mongoService.SendMatchRequest(sender,receiver,request.LetterCount);

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

            var result = await _mongoService.AcceptMatchRequest(receiver);

            if (result == null || receiver == null)
                return NotFound();
            if (result != null)
            {
                return Json(result.MatchIdentifier);
            }

            return BadRequest();
        }

        [HttpPost("PushSenderIdentifier", Name = "PushSenderIdentifier")]
        public async Task<IActionResult> PushSenderIdentifier([FromBody] string userName)
        {
            var receiver = await _userManager.FindByNameAsync(userName);

            var result = await _mongoService.GetMatchStatusforSender(receiver);

            if (!result) return NotFound();

            else return Json(result);
        }

    }
}
