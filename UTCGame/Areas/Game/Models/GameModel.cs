using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTCGame.Areas.Employee.Models;
using UTCGame.Areas.FolderMedia.Models;

namespace UTCGame.Areas.Game.Models
{
    public class GameModel
    {
        [Key]
        public Guid GameID { get; set; }
        [Required]
        [BindProperty]
        public required string GameName { get; set; }
        [Required]
        [BindProperty]
        public required float GamePrice { get; set; }
        public string? GameType { get; set; }
        public string? GamePlatform { get; set; }
        [Required]
        [BindProperty]
        public required DateTime GameReleaseDate { get; set; }
        public required Guid EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual required EmployeeModel Employee { get; set; }
        public required Guid FolderMediaID { get; set; }
        [ForeignKey("FolderMediaID")]
        public virtual required FolderMediaModel FolderMedia { get; set; }
        public bool IsGameActive { get; set; }
    }
}
