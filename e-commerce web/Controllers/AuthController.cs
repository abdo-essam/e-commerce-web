using e_commerce_web.Models.Domain;
using e_commerce_web.Models.Dto;
using e_commerce_web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenServices tokenServices;

        public AuthController(UserManager<ApplicationUser> userManager,ITokenServices tokenServices)
        {
            this.userManager = userManager;
            this.tokenServices = tokenServices;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AddRegisterRequestDto requestDto)
        {
            var user = new ApplicationUser()
            {
                UserName = requestDto.Name,
                Email = requestDto.Email,
                Address = requestDto.Address,
                PhoneNumber = requestDto.PhoneNumber
            };
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            var result = await userManager.CreateAsync(user, requestDto.Password);
            if (result.Succeeded)
            {

                var role = await userManager.AddToRoleAsync(user, "User");
                if (role.Succeeded)
                {
                    return Ok("User created successfully");
                }
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AddLoginRequestDto requestDto)
        {
            var user = await userManager.FindByEmailAsync(requestDto.Email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await userManager.CheckPasswordAsync(user, requestDto.Password);
            if (!result)
            {
                return BadRequest("Invalid password");
            }

            var roles = await userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "not assign"; 

            var token = tokenServices.CreateToken(user, userRole);

            return Ok(new
            {
                message = "Login successful",
                token = token
            });
        }

    }
}
