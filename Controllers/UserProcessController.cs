using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordleOnlineServer.Models;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProcessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserProcessController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {

   
            var user = new AppUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Phone,};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("Kullanıcı başarıyla kaydedildi.");
            }
            else
            {
                return BadRequest(result.Errors);
            }


        }
    }
}
