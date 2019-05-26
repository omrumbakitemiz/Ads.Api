using Ads.Api.Data;
using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Models
{
    public class Campaign
    {
        public string CampaignId { get; set; }

        public Company Company { get; set; }
        
        public string CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IPoint Location { get; set; }
        
        public Category Category { get; set; }
        
        public ICollection<UserCampaign> UserCampaigns { get; set; }

        public Campaign()
        {
            UserCampaigns = new Collection<UserCampaign>();
        }
    }
}
