using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace e_commerce_web.Models.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [ValidateNever]

        public ICollection<Product> Products { get; set; }
    }
}
