using API.Dtos;
using AutoMapper;
using Core.Entities.Product_Entities;

namespace API.Helpers
{
    public class ProductImagesResolver : IValueResolver<Product, ProductToReturnDto, string[]>
    {
        private readonly IConfiguration _configuration;

        public ProductImagesResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string[] Resolve(Product source, ProductToReturnDto destination, string[] destMember, ResolutionContext context)
        {
            string[] ImagesPath = new string[4];

            if (source.Images is not null)
            {
                ImagesPath[0] = $"{_configuration["ApiBaseUrl"]}/{source.Images[0]}";
                ImagesPath[1] = $"{_configuration["ApiBaseUrl"]}/{source.Images[1]}";
                ImagesPath[2] = $"{_configuration["ApiBaseUrl"]}/{source.Images[2]}";
                ImagesPath[3] = $"{_configuration["ApiBaseUrl"]}/{source.Images[3]}";
                return ImagesPath;
            }
            return ImagesPath;
        }
    }
}
