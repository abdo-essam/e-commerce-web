using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models.Dto
{
    public class AddLoginRequestDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
