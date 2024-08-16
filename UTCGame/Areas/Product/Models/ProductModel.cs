using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTCGame.Areas.Game.Models;

namespace UTCGame.Areas.Product.Models
{
    public class ProductModel
    {
        [Key]
        public Guid ProductID { get; set; }
        [Required]
        [BindProperty]
        public required string ProductName { get; set; }
        [Required]
        [BindProperty]
        public required float ProductPrice { get; set; }
        [Required]
        [BindProperty]
        [Range(0, 999999)]
        public required int ProductQuantity { get; set; }
        [Required]
        [BindProperty]
        public required DateTime ProductReleaseDate { get; set; }
        [Required]
        [BindProperty]
        public required Guid ProductTypeID { get; set; }
        [ForeignKey("ProductTypeID")]
        public virtual required ProductType ProductType { get; set; }
        [Required]
        [BindProperty]
        public required Guid GameID { get; set; }
        [ForeignKey("GameID")]
        public virtual required GameModel Game { get; set; }
        public bool IsProductActive { get; set; }
    }
}
