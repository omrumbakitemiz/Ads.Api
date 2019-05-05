using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;

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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var loginResource = await _userService.SignIn(user);

            return Ok(loginResource);
        }
    }
}