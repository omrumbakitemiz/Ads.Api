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
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public Company Company { get; set; }

        public IPoint Location { get; set; }

        public long Threshold { get; set; }

        public ICollection<UserCampaign> UserCampaigns { get; set; }

        public Campaign()
        {
            UserCampaigns = new Collection<UserCampaign>();
        }
    }
}
