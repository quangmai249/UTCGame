using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Employee.Models
{
    public class EmployeeModel
    {
        [Key]
        public Guid EmployeeID { get; set; }
        [Required]
        [BindProperty]
        [EmailAddress]
        public required string EmployeeEmail { get; set; }
        [Required]
        [BindProperty]
        [StringLength(24, MinimumLength = 8)]
        public required string EmployeePassword { get; set; }
        [Required]
        [BindProperty]
        public required Guid RegionID { get; set; }
        [ForeignKey("RegionID")]
        public virtual required Region Region { get; set; }
        public required Guid RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual required Role Role { get; set; }
        public bool IsEmployeeActive { get; set; }
    }
}
