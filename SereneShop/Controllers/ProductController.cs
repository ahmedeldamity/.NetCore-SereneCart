using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities.Product_Entities;
using Core.Interfaces.Services;
using Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecificationParameters specParams)
        {
            var products = await _productService.GetProductsAsync(specParams);

            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            
            var productsCount = await _productService.GetProductCount(specParams);

            return Ok(new PaginationToReturn<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, productsCount, productsDto)); 
        }
    }
}
