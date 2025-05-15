using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace e_commerce_web.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string?  Address{ get; set; }
        [ValidateNever]
        public Cart Cart { get; set; }
    }
}
