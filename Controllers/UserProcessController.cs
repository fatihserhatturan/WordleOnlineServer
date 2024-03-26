using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WordleOnlineServer.Models.Dtos;
using WordleOnlineServer.Models.MongoModels;
using WordleOnlineServer.Models.MsSqlModels;
using WordleOnlineServer.Options.Config;
using WordleOnlineServer.Services;

namespace WordleOnlineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProcessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtService _jwtService;

        public UserProcessController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
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
                var user = await _userManager.FindByNameAsync(model.UserName);
                var tokenString = _jwtService.CreateToken(user);    
                Console.WriteLine(tokenString);
                return Ok(new { Token = tokenString, Message = "Kullanıcı girişi başarılı" });
            }
            else
            {
                var errors = result.IsLockedOut ? "Hesap kilitlenmiş" : "Giriş başarısız";
                return BadRequest(errors);
            }   

        }

    }
}
