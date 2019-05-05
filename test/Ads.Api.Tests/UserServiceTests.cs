using System;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Ads.Api.Services;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Xunit;

namespace Ads.Api.Tests
{
    public class UserServiceTest
    {
        private static ICampaignService GetInMemoryCampaignService()
        {
            var builder = new DbContextOptionsBuilder<AdsDbContext>();
            var options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            AdsDbContext clubSystemDbContext = new AdsDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new CampaignService(clubSystemDbContext);
        }

        [Fact]
        public async Task ShouldAddCampaign()
        {
            // arrange
            var campaignService = GetInMemoryCampaignService();

            // act
            var campaign = await campaignService.Add(new Campaign
            {
                Name = "Campaign1",
                Threshold = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Description = "Desc",
                Location = new Point(100, 100, 200)
            });

            // assert
            var addedCampaign = campaignService.Get(campaign.Id);
            Assert.NotNull(campaign);
            Assert.NotNull(addedCampaign);
        }
    }
}