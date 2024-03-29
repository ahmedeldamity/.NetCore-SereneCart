﻿using Core.Entities.Product_Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications.ProductSpecifications;

namespace Service
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecificationParameters specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams); // to set data in properties which exist in BaseSpecification 
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<int> GetProductCount(ProductSpecificationParameters specParams)
        {
            var spec = new ProductCountSpecification(specParams);
            var productsCount = await _unitOfWork.Repository<Product>().GetCountAsync(spec);
            return productsCount;
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            return product;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return brands;
        }
        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
            return categories;
        }
    }
}
