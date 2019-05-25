using System.Threading.Tasks;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _companyService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _companyService.Add(company);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Company company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _companyService.Edit(company);
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _companyService.Delete(id);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _companyService.All());
        }
    }
}