using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Api.Configuration;
using News.Api.Data;
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
        private ApplicationDbContext _db;
        public AuthController(AppConfiguration config, ApplicationDbContext db)
        {
            _config = config;
            _db = db;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IResult Login(User user)
        {
            if (user == null)
            {
                return Results.BadRequest("InvalidData");
            }
            if (_db.Users.Any(x => x.Username == user.Username && x.Password == user.Password))
            {
                string token = generateJwtToken(user.Username, user.Password);
                return Results.Ok(token);

            }
            // There isn't a username and password that match
            return Results.Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("/test")]
        public IResult Test()
        {

            var usersNamedJohn = _db.Users.Where(user => user.Username == "John");
            return Results.Ok(usersNamedJohn);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IResult Register([FromBody] User user)
        {
            if(user == null)
            {
                return Results.BadRequest("InvalidData");
            }
            if(_db.Users.Any(u => u.Username == user.Username))
            {
                return Results.BadRequest("Username already exists");
            }
            try
            {
                User newUser = new User(user.Username, user.Password);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                string token = generateJwtToken(user.Username, user.Password);
                return Results.Ok(token);
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private bool ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_config.AuthSecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey= true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "https://your-news.com",
                    ValidateAudience = true,
                    ValidAudience = "https://your-news.com",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch (SecurityTokenException) { return false; }
        }
        private string generateJwtToken(string username, string password)
        {

            var issuer = "https://your-news.com";
            var audience = "https://your-news.com";
            var key = Encoding.ASCII.GetBytes(_config.AuthSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Email, username),
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
            return stringToken;
        }
    }
}
