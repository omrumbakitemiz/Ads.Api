using GeoAPI.Geometries;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Api.Models
{
    [Table("Compaines")]
    public class Company
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public IPoint Location { get; set; }

        public long DefaultThreshold { get; set; }

        public Collection<Campaign> MyProperty { get; set; }
    }
}