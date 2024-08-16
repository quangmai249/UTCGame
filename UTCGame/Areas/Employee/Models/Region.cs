using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Employee.Models
{
    public class Region
    {
        [Key]
        public Guid RegionID { get; set; }
        [Required]
        [BindProperty]
        [StringLength(50, MinimumLength = 1)]
        public required string RegionName { get; set; }
        public bool IsRegionActive { get; set; }
    }
}
