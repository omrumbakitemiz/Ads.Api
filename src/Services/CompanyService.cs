using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ads.Api.Data;
using Ads.Api.Interfaces;
using Ads.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace Ads.Api.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AdsDbContext _context;
        private readonly ILogger<CampaignService> _logger;

        public CompanyService(AdsDbContext context, ILogger<CampaignService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(Company company)
        {
            company.Location = new Point(company.X, company.Y)
            {
                SRID = 4326
            };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Company Add: ", company);
        }

        public async Task<Company> Get(string id)
        {
            var company = await _context.Companies.FindAsync(id);
            _logger.Log(LogLevel.Information, "Company Get: ", company);

            return company;
        }

        public async Task Edit(Company company)
        {
            var foundedCompany = await _context.Companies.FindAsync(company.Id);

            if (foundedCompany == null) throw new Exception("Company Not Found");

            _context.Companies.Update(company);

            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Company Edit: ", company);
        }

        public async Task Delete(string id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null) throw new Exception("Company Not Found");

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "Company Delete: ", company);
        }

        public async Task<List<Company>> All()
        {
            var companies = await _context.Companies.ToListAsync();
            _logger.Log(LogLevel.Information, "Company All: ", companies);

            return companies;
        }
    }
}