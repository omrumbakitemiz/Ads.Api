using System.Threading.Tasks;
using Ads.Api.Controllers;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ads.Api.Tests
{
    public class CampaignControllerTests
    {
        [Fact]
        public async Task GetReturnsOneCampaign()
        {
            // arrange
            var mockCampaignService = new Mock<ICampaignService>();
            mockCampaignService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new Campaign()));
            var campaignController = new CampaignController(mockCampaignService.Object);
            
            // act
            var result = await campaignController.Get("ID");

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actionResult = Assert.IsAssignableFrom<IActionResult>(viewResult);
        }
    }
}