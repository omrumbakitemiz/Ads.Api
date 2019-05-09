using System;
using System.Linq;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Ads.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;
using Xunit;

namespace Ads.Api.Tests
{
    public class UserServiceTest
    {
        private readonly Mock<ILogger<CampaignService>> _mockILogger;

        public UserServiceTest()
        {
            _mockILogger = new Mock<ILogger<CampaignService>>();
        }

        [Fact]
        public async Task ShouldAddCampaign()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();
            var campaign = new Campaign
            {
                Name = "Campaign1",
                Threshold = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Description = "Desc",
                Location = new Point(100, 100, 200)
            };

            // act
            await campaignService.Add(campaign);

            // assert
            var addedCampaign = campaignService.Get(campaign.Id);
            Assert.NotNull(campaign);
            Assert.NotNull(addedCampaign);
        }

        [Fact]
        public async Task ShouldGetAllCampaigns()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();
            
            // act
            var campaign = new Campaign
            {
                Name = "Campaign1",
                Threshold = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Description = "Desc",
                Location = new Point(100, 100, 200)
            };
            await campaignService.Add(campaign);
            
            var allCampaings = await campaignService.All();

            // assert
            Assert.NotNull(allCampaings);
            Assert.Single(allCampaings);
            Assert.True(allCampaings.First().Equals(campaign));
        }

        [Fact]
        public async Task ShouldDeleteCampaign()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();
            
            // act
            var campaign = new Campaign
            {
                Name = "Campaign1",
                Threshold = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Description = "Desc",
                Location = new Point(100, 100, 200)
            };
            await campaignService.Add(campaign);
            await campaignService.Delete(campaign.Id);
            
            var allCampaings = await campaignService.All();
            
            // assert
            Assert.Empty(allCampaings);
        }
        
        [Fact]
        public async Task ShouldThrowCampaignNotFoundException()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();
            
            // act & assert
            await Assert.ThrowsAsync<Exception>(async () => await campaignService.Delete("WRONG_ID"));
        }

        [Fact]
        public async Task ShouldEditCampaign()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();

            // act
            var campaign = new Campaign
            {
                Name = "Campaign1",
                Threshold = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Description = "Desc",
                Location = new Point(100, 100, 200)
            };

            await campaignService.Add(campaign);

            var updatedCampaign = campaign;
            updatedCampaign.Description = "Updated desc";
            updatedCampaign.EndDate = DateTime.Today.AddDays(7);
            updatedCampaign.Threshold = 20;

            await campaignService.Edit(updatedCampaign);

            var editedCampaign = await campaignService.Get(updatedCampaign.Id);

            // assert
            Assert.Equal(updatedCampaign.Description, editedCampaign.Description);
            Assert.Equal(updatedCampaign.EndDate, editedCampaign.EndDate);
            Assert.Equal(updatedCampaign.Threshold, editedCampaign.Threshold);
        }
        
        
        private ICampaignService GetInMemoryCampaignService()
        {
            var builder = new DbContextOptionsBuilder<AdsDbContext>();
            var options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            AdsDbContext clubSystemDbContext = new AdsDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new CampaignService(clubSystemDbContext, _mockILogger.Object);
        }
    }
}