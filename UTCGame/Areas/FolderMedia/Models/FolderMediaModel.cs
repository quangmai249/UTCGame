using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.FolderMedia.Models
{
    public class FolderMediaModel
    {
        [Key]
        public Guid FolderMediaID { get; set; }
        [Required]
        [BindProperty]
        public required string FolderMediaName { get; set; }
        public bool IsAvtive { get; set; }
    }
}
