using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ads.Api.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly AdsDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CampaignService> _logger;

        public CampaignService(IConfiguration configuration, ILogger<CampaignService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        public CampaignService(AdsDbContext context)
        {
            _context = context;
        }

        public async Task Add(Campaign campaign)
        { 
            await _context.Campaigns.AddAsync(campaign);
            await _context.SaveChangesAsync();
        }

        public async Task<Campaign> Get(string id)
        {
            return await _context.Campaigns.FindAsync(id);
        }

        public async Task<List<Campaign>> All()
        {
            return await _context.Campaigns.ToListAsync();
        }

        public async Task Delete(string id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);

            if (campaign == null) throw new Exception("Campaign Not Found");
            
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
        }
    }
}