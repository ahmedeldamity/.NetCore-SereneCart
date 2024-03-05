﻿using Core.Entities.Order_Entities;
using Core.Entities.Product_Entities;
using Core.Entities.Wishlist_Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -- Old Way   
            //modelBuilder.ApplyConfiguration(new ProductConfigurations());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfigurations());
            //modelBuilder.ApplyConfiguration(new ProductCategoryConfigurations());

            // -- New Way
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderDeliveryMethod> OrderDeliveryMethods { get; set; }
    }
}
