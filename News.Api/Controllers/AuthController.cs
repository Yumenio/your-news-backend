using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Api.Configuration;
using News.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace News.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private AppConfiguration _config;
        public AuthController(AppConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("/login")]
        public IResult Login(User user)
        {
            if(user.Username == "test" && user.Password == "1234")
            {
                var issuer = "https://your-news.com";
                var audience = "https://your-news.com";
                var key = Encoding.ASCII.GetBytes(_config.AuthSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Email, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(stringToken);

            }
            return Results.Unauthorized();
        }
    }
}
