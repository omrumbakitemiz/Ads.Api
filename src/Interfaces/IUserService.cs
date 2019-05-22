using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Ads.Api.Interfaces
{
    public interface IUserService
    {
        Task<SignInResource> SignIn(User user);
        Task<IdentityResult> SignUp(User user);
    }
}