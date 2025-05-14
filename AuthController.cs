using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthWebAPIDemo.Entities;
using AuthWebAPIDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private static User? user = new();

        public IConfiguration Configuration { get; }

        [HttpPost("Register")]
        public ActionResult<User?>Register(UserDto request)
        {
            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<String?> Login(UserDto request)
        {
            if (user.Username != request.Username)
                return BadRequest("User not found");
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
                return BadRequest("Wrong PAssword");
            string token = CreateToken(user); //details passed in the basis of user
            return Ok(token);
        }

        private String CreateToken(User user)
        {
            var claims = new List<Claim>// in claims- who you are? All details ,Access you have? all the things available in claims
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey( //Geneate key for encryption
                Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:Token")!)); //add configuration in ctor., fetch value of appsettings:Token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); // sign it by using encryption Algorithm , 512bits- 64Bytes 
            var tokenDescriptor = new JwtSecurityToken( //Create Token
                issuer: Configuration.GetValue<string>("AppSettings:Issuer"),
                audience: Configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); //write Token generate String Token, return in Login token

        }
    } 
}
