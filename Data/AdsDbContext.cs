﻿using Ads.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ads.Api.Data
{
    public class AdsDbContext : DbContext
    {
        public AdsDbContext(DbContextOptions<AdsDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCampaign>().HasKey(userCampaign => new { userCampaign.UserId, userCampaign.CampaignId });
        }
    }
}
