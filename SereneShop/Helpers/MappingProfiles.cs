using API.Dtos;
using AutoMapper;
using Core.Entities.Basket_Entities;
using Core.Entities.Product_Entities;
using Talabat.APIs.Helpers;

namespace API.Helpers
{
    public class MappingProfiles: Profile
    {
        //private readonly IConfiguration _configuration;
        public MappingProfiles(/*IConfiguration configuration*/)
        {
            //_configuration = configuration;

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category))
                // -- We wanted to bring configuration to bring "ApiBaseUrl From appsetting.json 
                // -- but this isn't work because when we register automapper in program class 
                // -- it create this class with parameter less constractor 
                // -- so it will chain on the parameter less constractor and didn't see this constractor
                // -- so i commented the below line and configuration
                //.ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{_configuration["ApiBaseUrl"]}/{s.PictureUrl}"))
                // -- the solution of this issue is: instead of using MapFrom I use MapFrom<"class inherit from IValueResolver<sourse, destination, member>">
                .ForMember(d => d.ImageCover, o => o.MapFrom<ProductImageCoverResolver>())
                .ForMember(d => d.Images, o => o.MapFrom<ProductImagesResolver>());

            CreateMap<BasketDto, Basket>();

            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Basket, BasketToReturnDto>();

            CreateMap<BasketItem, BasketItemToReturnDto>();

        }
    }
}
