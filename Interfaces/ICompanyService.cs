using System.Collections.Generic;
using System.Threading.Tasks;
using Ads.Api.Models;

namespace Ads.Api.Interfaces
{
    public interface ICompanyService
    {
        Task Add(Company campaign);

        Task<Company> Get(string id);

        Task<List<Company>> All();
        
        Task Delete(string id);

        Task Edit(Company campaign);
    }
}