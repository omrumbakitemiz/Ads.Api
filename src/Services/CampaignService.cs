using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
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

        public Task<Campaign> Add(Campaign campaign)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Campaign>> All()
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}