using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace e_commerce_web.Models.Domain
{
    public class CartItem
    {
        public int  CartItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        [Column(TypeName = "decimal(10, 2)")]
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal Price { get; set; }

        [ValidateNever]
        public Cart Cart { get; set; }
        public Guid CartId { get; set; }

        [ValidateNever]
        public ICollection<Product> products { get; set; } = new List<Product>();







    }
}
