using Core.Entities.Identity_Entities;
using Core.Entities.Wishlist_Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Identity
{
    public class IdentityContext: IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        // In this method we override OnModelCreating which exist in base class
        // so we need to call it
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAddress>()
                .ToTable("Addresses");

            builder.Entity<AppUser>()
                .HasOne(P => P.Wishlist)
                .WithOne();

            builder.Entity<WishlistItem>()
                .Property(e => e.Images)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                );

            builder.Entity<WishlistItem>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<WishlistItem>()
                .Property(p => p.Quantity)
                .HasColumnType("decimal(18,2)");

            builder.Entity<WishlistItem>()
                .Property(p => p.RatingsAverage)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
    }
}
