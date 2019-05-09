using System.Threading.Tasks;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        public async Task<IActionResult> Get([FromBody] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _campaignService.Get(id));
        }

        public async Task<IActionResult> Post([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Add(campaign);
            return Ok();
        }

        public async Task<IActionResult> Edit([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Edit(campaign);
            return Ok();
        }

        public async Task<IActionResult> Delete([FromBody]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Delete(id);
            return Ok();
        }

        public async Task<IActionResult> All()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _campaignService.All());
        }
    }
}