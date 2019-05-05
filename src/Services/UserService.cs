using System.Linq;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Errors;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using News.Api.Extensions;

namespace Ads.Api.Services
{
    [Authorize]
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
        }

        [AllowAnonymous]
        public async Task<SignInResource> SignIn(User user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);

            if (!result.Succeeded)
            {
                _logger.LogError("Login Failed");
                throw new LoginException("Login Failed");
            }
            
            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == user.UserName);
            var signInResource = new SignInResource
            {
                Token = _jwtTokenGenerator.GenerateJwtToken(user.UserName, appUser),
                User = appUser
            };
            return signInResource;
        }

        [AllowAnonymous]
        public Task<SignUpResource> SignUp(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}