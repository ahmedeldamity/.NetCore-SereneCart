using Core.Entities.Wishlist_Entities;

namespace API.Dtos
{
    public class WishlistDto
    {
        public string Id { get; set; }

        public ICollection<WishlistItem> Items { get; set; }
    }
}
