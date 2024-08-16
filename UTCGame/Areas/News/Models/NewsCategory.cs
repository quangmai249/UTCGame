using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.News.Models
{
    public class NewsCategory
    {
        [Key]
        public Guid NewsCategoryID { get; set; }
        [Required]
        [BindProperty]
        public required string NewsCategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
