using e_commerce_web.Models.Domain;

namespace e_commerce_web.Services.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(ApplicationUser user,string role);

    }
}
