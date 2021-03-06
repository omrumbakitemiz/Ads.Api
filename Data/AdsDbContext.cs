﻿using Ads.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ads.Api.Data
{
    public class AdsDbContext : IdentityDbContext<User>
    {
        public AdsDbContext(DbContextOptions<AdsDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().Property(company => company.CompanyId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Campaign>().Property(campaign => campaign.CampaignId).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<UserCampaign>()
                .HasKey(userCampaign => new {userCampaign.UserId, userCampaign.CampaignId});

            modelBuilder.Entity<Campaign>().Property(c => c.Category).HasConversion<string>();
        }
    }
}