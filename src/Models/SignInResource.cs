using Ads.Api.Data;

namespace Ads.Api.Models
{
    public class SignInResource
    {
        public string Token { get; set; }

        public User User { get; set; }
    }
}
