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
        private readonly MongoService _mongoService;
        private readonly DictionaryService _dictionaryService;
        public MatchController(UserManager<AppUser> userManager, MongoService mongoService, DictionaryService dictionaryService)
        {
            _userManager = userManager;
            _mongoService = mongoService;
            _dictionaryService = dictionaryService;
        }


        [HttpPost("GetUserLetter", Name = "GetUserLetter")]
        public async Task<IActionResult> GetUserLetter([FromBody] GetUserLetterDto dto)
        {

            var user = await _userManager.FindByNameAsync(dto.Username);
            var match = await _mongoService.GetMatchByIdentifier(dto.MatchIdentifier);

            var letterEnable = await _dictionaryService.IsWordEnableForUsing(dto.Letter);

            if (!letterEnable)
            {
                return BadRequest("We couldnt find this word in our system");
            }

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

        [HttpPost("UserTimeOut", Name = "UserTimeOut")]
        public async Task<IActionResult> UserTimeOut([FromBody] UserMatchDTO dTO)
        {
            var user = await _userManager.FindByNameAsync(dTO.userName);

            var match = await _mongoService.GetMatchByIdentifier(dTO.matchIdentifier);

            if (match.UserSender.UserName == user.UserName)
            {
                string value = await _mongoService.UserFailByTimeOut(dTO, "sender");

                return Json(value);
            }
            if (match.UserReceiver.UserName == user.UserName)
            {
                string value = await _mongoService.UserFailByTimeOut(dTO, "receiver");

                return Json(value);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("SwitchUserLetter", Name = "SwitchUserLetter")]
        public async Task<IActionResult> SwitchUserLetter([FromBody] GetUserLetterDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            var match = await _mongoService.GetMatchByIdentifier(dto.MatchIdentifier);

            if (match.UserSender.UserName == user.UserName)
            {

                string value =  await _mongoService.SwitchMatchLetter(dto, "sender");

                if(value == "fail")
                {
                    return BadRequest("match fail , you win");
                }

                return Json(value);
            }
            if (match.UserReceiver.UserName == user.UserName)
            {
                string value = await _mongoService.SwitchMatchLetter(dto, "receiver");

                if (value == "fail")
                {
                    return BadRequest("match fail , you win");
                }

                return Json(value);
            }
            else
            {
                return NotFound();
            }

        }

 


    }
}
