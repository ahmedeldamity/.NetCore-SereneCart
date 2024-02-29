using Core.Entities.Product_Entities;
using Core.Specifications.ProductSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecificationParameters specParams);
        Task<int> GetProductCount(ProductSpecificationParameters specParams);
        Task<Product?> GetProductAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
    }
}
