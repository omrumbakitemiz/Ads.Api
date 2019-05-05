using System.Collections.Generic;
using System.Threading.Tasks;
using Ads.Api.Models;

namespace Ads.Api.Interfaces
{
    public interface ICampaignService
    {
        Task Add(Campaign campaign);

        Task<Campaign> Get(string id);

        Task<List<Campaign>> All();
        
        Task Delete(string id);
    }
}