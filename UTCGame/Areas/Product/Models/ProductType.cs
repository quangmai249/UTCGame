using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UTCGame.Areas.Product.Models
{
    public class ProductType
    {
        [Key]
        public Guid ProductTypeID { get; set; }
        [Required]
        [BindProperty]
        public required string ProductTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
