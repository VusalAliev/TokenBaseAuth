using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TokenBaseAuth.DTOs;
using TokenBaseAuth.Entites;
using TokenBaseAuth.Models;
using TokenBaseAuth.Services.Interfaces;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TokenBaseAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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
            Token token = _tokenHandler.CreateAccessToken(10);
            return new ResponseModel { IsSuccess = true,Token=token };
        }
    }
}
