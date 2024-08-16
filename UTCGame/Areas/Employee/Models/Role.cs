using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Employee.Models
{
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }
        [Required]
        [BindProperty]
        [StringLength(50, MinimumLength = 3)]
        public required string RoleName { get; set; }
        public bool IsRoleActive { get; set; }
    }
}
