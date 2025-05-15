using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models.Dto
{
    public class AddRegisterRequestDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }



    }
}
