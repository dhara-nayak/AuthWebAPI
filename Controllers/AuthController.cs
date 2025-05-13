using AuthWebAPIDemo.Entities;
using AuthWebAPIDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static User? user = new();
        [HttpPost("Register")]
        public ActionResult<User?>Register(UserDto request)
        {
            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User?> Login(UserDto request)
        {
            if (user.Username != request.Username)
                return BadRequest("User not found");
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
                return BadRequest("Wrong PAssword");
            return Ok(user);
        }
    }
}
