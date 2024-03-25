using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordleOnlineServer.Models.Dtos;
using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProcessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserProcessController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {

   
            var user = new AppUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Phone,EmailConfirmed = true};
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

        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);


            if (result.Succeeded)
            {
                return Ok("Kullanıcı Girişi Başarılı");
            }
            else
            {
                var errors = result.IsLockedOut ? "Hesap kilitlenmiş" : "Giriş başarısız";
                return BadRequest(errors);
            }   

        }

    }
}
