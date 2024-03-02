using Core.Entities.Wishlist_Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity_Entities
{
    public class AppUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public UserAddress Address { get; set; }
        public string WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
