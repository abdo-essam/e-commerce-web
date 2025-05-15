using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models.Dto
{
    public class UpdateCategoryRequestDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }
    }
}
