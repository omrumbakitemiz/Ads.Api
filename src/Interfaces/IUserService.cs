using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Models;

namespace Ads.Api.Interfaces
{
    public interface IUserService
    {
        Task<SignInResource> SignIn(User user);
        Task<SignUpResource> SignUp(User user);
    }
}