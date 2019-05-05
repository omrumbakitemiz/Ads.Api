using System;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ads.Api.Tests
{
    public class UserServiceTest
    {
        private IUserService GetInMemoryUserService()
        {
            DbContextOptions<AdsDbContext> options;
            var builder = new DbContextOptionsBuilder<AdsDbContext>();
            options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            AdsDbContext clubSystemDbContext = new AdsDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new UserService(clubSystemDbContext);
        }
        
        [Fact]
        public void ShouldThrowLoginError()
        {
            // arrange

            // act

            // assert
        }
    }
}
