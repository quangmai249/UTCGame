using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Game.Models
{
    public class GameType
    {
        [Key]
        public Guid GameTypeID { get; set; }
        [Required]
        [BindProperty]
        public required string GameTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
