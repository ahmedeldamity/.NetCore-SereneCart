using API.Dtos;
using API.Errors;
using Core.Entities.Basket_Entities;
using Core.Entities.Identity_Entities;
using Core.Entities.Wishlist_Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Identity;
using System.Security.Claims;

namespace API.Controllers
{
    public class WishlistController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWishlistService _wishlistService;
        private readonly IdentityContext _identityContext;

        public WishlistController(UserManager<AppUser> userManager, IWishlistService wishlistService,
            IdentityContext identityContext)
        {
            _userManager = userManager;
            _wishlistService = wishlistService;
            _identityContext = identityContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Wishlist>> GetUserWishlist()
        {
            return Ok(await UserWishlist());
        }

        [Authorize]
        [HttpPost]  
        public async Task<ActionResult<Wishlist>> AddProductToWishlist(WishlistItem wishlistItem)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            wishlistItem.WishlistId = user.WishlistId;

            var product = await _wishlistService.AddProductToWishlistAsync(wishlistItem);

            if(product is null)
                return BadRequest(new ApiResponse(400));

            var wishlist = await UserWishlist();

            if (wishlist is null)
                return BadRequest(new ApiResponse(400));

            return Ok(wishlist);
        }

        public async Task<Wishlist> UserWishlist()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var wishlistItems = await _identityContext.WishlistItems.Where(w => w.WishlistId == user.WishlistId).ToListAsync();
            var wishlist = await _identityContext.Wishlists.Where(w => w.Id == user.WishlistId).FirstOrDefaultAsync();
            wishlist.Items = wishlistItems;
            return wishlist;
        }

    }
}
