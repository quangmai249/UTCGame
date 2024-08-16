using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTCGame.Areas.FolderMedia.Models;

namespace UTCGame.Areas.News.Models
{
    public class NewEvent
    {
        [Key]
        public Guid NewEventID { get; set; }
        [Required]
        [BindProperty]
        public required string NewEventTitle { get; set; }
        [Required]
        [BindProperty]
        public required string NewEventDetail { get; set; }
        [Required]
        [BindProperty]
        public required string NewEventDateTime { get; set; }
        [Required]
        [BindProperty]
        public required Guid NewsCategoryID { get; set; }
        [ForeignKey("NewsCategoryID")]
        public virtual required NewsCategory NewsCategory { get; set; }
        [Required]
        [BindProperty]
        public required Guid FolderMediaID { get; set; }
        [ForeignKey("FolderMediaID")]
        public virtual required FolderMediaModel FolderMediaModel { get; set; }
        public bool IsActive { get; set; }
    }
}
