using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Ads.Api.Models
{
    [Table("Compaines")]
    public class Company
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public Point Location { get; set; }

        [NotMapped]
        public double X { get; set; }

        [NotMapped]
        public double Y { get; set; }

        public long DefaultThreshold { get; set; }

        public Collection<Campaign> Campaigns { get; set; }

        public Company()
        {
            Campaigns = new Collection<Campaign>();
        }
    }
}