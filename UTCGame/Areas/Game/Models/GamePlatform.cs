using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Game.Models
{
    public class GamePlatform
    {
        [Key]
        public Guid GamePlatformID { get; set; }
        [Required]
        [BindProperty]
        public required string GamePlatformName { get; set; }
        public string? GamePlatformLink { get; set; }
        public bool IsActive { get; set; }
    }
}
