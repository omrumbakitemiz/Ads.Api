using System.Threading.Tasks;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _campaignService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Add(campaign);
            return Ok(campaign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Edit(campaign);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.Delete(id);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _campaignService.All());
        }
    }
}