namespace Core.Entities.Product_Entities
{
    public class Product: EntityWithIntId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageCover { get; set; }
        public string[] Images { get; set; }
        public decimal Quantity { get; set; }
        public decimal RatingsAverage { get; set; } 
        public int BrandId { get; set; } // FK - ProductBrand - But We Don't Named ProductBrandId So EF Don't Know This FK So We Make It In Fluent API
        public ProductBrand Brand { get; set; } // Navigational Property
        public int CategoryId { get; set; } // FK - ProductCategory - But We Don't Named ProductCategoryId So EF Don't Know This FK So We Make It In Fluent API
        public ProductCategory Category { get; set; } // Navigational Property
    }
}
