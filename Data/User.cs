using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ads.Api.Data
{
    public class User : IdentityUser
    {
        public ICollection<UserCampaign> UserCampaigns { get; set; }
    }
}
