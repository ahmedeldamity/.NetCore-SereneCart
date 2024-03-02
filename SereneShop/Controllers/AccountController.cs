using API.Dtos;
using API.Errors;
using Core.Entities.Identity_Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IWishlistService _wishlistService;

        public AccountController(UserManager<AppUser> userManager, IAuthService authService,
            SignInManager<AppUser> signInManager, IWishlistService wishlistService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _wishlistService = wishlistService;
            
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            return Ok(new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager, user.WishlistId)
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppUserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email has already been used" } });

            // Create Wishlist
            var wishlist = await _wishlistService.CreateWishlistAsync();

            if(wishlist is null)
                return BadRequest(new ApiResponse(400));

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                Wishlist = wishlist,
                WishlistId = wishlist.Id
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded is false)
                return BadRequest(new ApiResponse(400));

            return Ok(new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager, wishlist.Id)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager, user.WishlistId)
            });
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
