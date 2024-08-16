using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTCGame.Areas.Employee.Models;

namespace UTCGame.Areas.Recruit.Models
{
    public class RecruitModel
    {
        [Key]
        public Guid RecruitID { get; set; }
        [Required]
        [BindProperty]
        public required string RecruitName { get; set; }
        [Required]
        [BindProperty]
        public required Guid RegionID { get; set; }
        [ForeignKey("RegionID")]
        public virtual required Region Region { get; set; }
        public bool IsActive { get; set; }
    }
}
