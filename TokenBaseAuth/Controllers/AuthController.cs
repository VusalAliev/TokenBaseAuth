using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TokenBaseAuth.DTOs;
using TokenBaseAuth.Entites;
using TokenBaseAuth.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TokenBaseAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<ResponseModel> Signup(CreateUserDTO createUserDTO)
        {
            IdentityResult createResult = await _userManager.CreateAsync(new()
            {
                UserName = createUserDTO.Username,
                Email = createUserDTO.Email,
                FullName = createUserDTO.FullName,
            }, createUserDTO.Password);
            if (createResult.Succeeded)
            {
                return new ResponseModel { IsSuccess = true };
            }
            else
            {
                return new ResponseModel { ErrorMessage = "An error occured", IsSuccess = false };
            }
        }
        [HttpPost("Login")]
        public async Task<ResponseModel> Login(LoginDTO loginDTO)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);

            if (user == null) user = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);

            if (user == null) return new ResponseModel { IsSuccess = false, ErrorMessage = "Kullanıcı veya şifre hatalı..." };

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (result.Succeeded)
            {
                //authorities
            }
            return new ResponseModel { IsSuccess = true };
        }
    }
}
