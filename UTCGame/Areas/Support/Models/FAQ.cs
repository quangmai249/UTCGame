using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Support.Models
{
    public class FAQ
    {
        [Key]
        public Guid FAQ_ID { get; set; }
        [Required]
        [BindProperty]
        public required string FAQ_Title { get; set; }
        [Required]
        [BindProperty]
        public required string FAQ_Detail { get; set; }
        public bool IsActive { get; set; }
    }
}
