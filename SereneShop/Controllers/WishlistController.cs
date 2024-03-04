using API.Errors;
using Core.Entities.Identity_Entities;
using Core.Entities.Wishlist_Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
            var wishlist = await UserWishlist();

            if (wishlist is null)
                return BadRequest(new ApiResponse(400));

            wishlistItem.WishlistId = wishlist.Id;

            var product = await _wishlistService.AddProductToWishlistAsync(wishlistItem);

            if(product is null)
                return BadRequest(new ApiResponse(400));

            return Ok(wishlist);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wishlist>> RemoveProductFromWishlistAsync(int id)
        {
            var product = await _identityContext.WishlistItems.FindAsync(id);

            if(product is null)
                return BadRequest(new ApiResponse(400));

            var returnProduct = await _wishlistService.RemoveProductFromWishlistAsync(product);

            if(returnProduct is null)
                return BadRequest(new ApiResponse(400));

            var wishlist = await UserWishlist();

            return Ok(wishlist);
        }

        public async Task<Wishlist> UserWishlist()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var wishlist = await _identityContext.Wishlists.Include(I => I.Items).FirstOrDefaultAsync((prod) => prod.Id == user.WishlistId);
            return wishlist;
        }
    
    }
}