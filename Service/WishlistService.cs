using Core.Entities.Wishlist_Entities;
using Core.Interfaces.Services;
using Repository.Identity;

namespace Service
{
    public class WishlistService: IWishlistService
    {
        private readonly IdentityContext _identityContext;

        public WishlistService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<Wishlist?> CreateWishlistAsync()
        {
            var wishlist = new Wishlist();

            await _identityContext.Wishlists.AddAsync(wishlist);

            var result = await _identityContext.SaveChangesAsync();

            if (result <= 0)
                return null;

            return wishlist;
        }

        public async Task<WishlistItem?> AddProductToWishlistAsync(WishlistItem product)
        {
            await _identityContext.WishlistItems.AddAsync(product);

            var result = await _identityContext.SaveChangesAsync();

            if (result <= 0)
                return null;

            return product;
        }

        public async Task<WishlistItem?> RemoveProductFromWishlistAsync(WishlistItem product)
        {
            _identityContext.Remove(product);

            var result = await _identityContext.SaveChangesAsync();

            if (result <= 0)
                return null;

            return product;
        }

    }
}
