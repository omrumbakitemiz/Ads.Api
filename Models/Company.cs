using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Api.Models
{
    [Table("Compaines")]
    public class Company
    {
        public Company()
        {
            Campaigns = new List<Campaign>();
        }
        
        public List<Campaign> Campaigns { get; set; }
        
        public string CompanyId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

//        public Point Location { get; set; }

        [NotMapped]
        public double X { get; set; }

        [NotMapped]
        public double Y { get; set; }
    }
}