namespace Core.Entities.Wishlist_Entities
{
    public class Wishlist: EntityWithStrId
    {
        public ICollection<WishlistItem> Items { get; set; } = new HashSet<WishlistItem>();

        public Wishlist()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
