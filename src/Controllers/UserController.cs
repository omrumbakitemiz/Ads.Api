using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var signInResource = await _userService.SignIn(user);

            return Ok(signInResource);
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var result = await _userService.SignUp(new User
                {UserName = signUpDto.Username, PasswordHash = signUpDto.Password});
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}