using Core.Entities.Wishlist_Entities;

namespace Core.Interfaces.Services
{
    public interface IWishlistService
    {
        Task<Wishlist?> CreateWishlistAsync();
        Task<WishlistItem?> AddProductToWishlistAsync(WishlistItem product);
    }
}
