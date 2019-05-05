using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ads.Api.Data
{
    public class User : IdentityUser
    {
        public ICollection<UserCampaign> UserCampaigns { get; set; }
    }
}
