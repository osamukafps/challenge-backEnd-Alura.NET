using AluraFlixChallenge.API.Entities;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Name, model.Password);

            if (user == null)
                return NotFound(new { message = "Invalid user or password" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token,
            };
        }
    }
}
