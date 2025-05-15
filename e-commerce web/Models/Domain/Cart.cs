using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace e_commerce_web.Models.Domain
{
    public class Cart
    {
        public Guid Id { get; set; }
        [ValidateNever]
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

        [ValidateNever]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
