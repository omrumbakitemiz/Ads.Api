using Ads.Api.Models;

namespace Ads.Api.Data
{
    public class UserCampaign
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Campaign Campaign { get; set; }
        public string CampaignId { get; set; }
    }
}
