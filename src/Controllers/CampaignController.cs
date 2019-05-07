using Ads.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    public class CampaignController : ControllerBase
    {
        public IActionResult Get([FromBody] string id)
        {
            return Ok();
        }

        public IActionResult Post([FromBody] Campaign campaign)
        {
            return Ok();
        }

        public IActionResult Edit([FromBody] Campaign campaign)
        {
            return Ok();
        }

        public IActionResult Delete([FromBody]string id)
        {
            return Ok();
        }

        public IActionResult All()
        {
            return Ok();
        }
    }
}