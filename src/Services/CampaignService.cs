using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ads.Api.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly AdsDbContext _context;
        private readonly ILogger<CampaignService> _logger;

        public CampaignService(AdsDbContext context, ILogger<CampaignService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(Campaign campaign)
        { 
            await _context.Campaigns.AddAsync(campaign);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Campaign Add: ", campaign);
        }

        public async Task<Campaign> Get(string id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            _logger.Log(LogLevel.Information, "Campaign Get: ", campaign);

            return campaign;
        }

        public async Task<List<Campaign>> All()
        {
            var campaigns = await _context.Campaigns.ToListAsync();
            _logger.Log(LogLevel.Information, "Campaign All: ", campaigns);

            return campaigns;
        }

        public async Task Delete(string id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);

            if (campaign == null) throw new Exception("Campaign Not Found");
            
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Campaign Delete: ", campaign);
        }

        public async Task Edit(Campaign campaign)
        {
            var foundedCampaign = await _context.Campaigns.FindAsync(campaign.Id);

            if (foundedCampaign == null) throw new Exception("Campaign Not Found");

            _context.Campaigns.Update(campaign);

            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Campaign Edit: ", campaign);
        }
    }
}