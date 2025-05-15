using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models.Dto
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
