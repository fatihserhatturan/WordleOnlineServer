using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WordleOnlineServer.Models.MsSqlModels;
using WordleOnlineServer.Options.Config;

namespace WordleOnlineServer.Services
{
    public  class JwtService 
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generate(AppUser user)
        {
            var claims = new Claim[] {
            
                new (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new (JwtRegisteredClaimNames.Email,user.Email),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
