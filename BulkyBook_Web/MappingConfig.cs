using AutoMapper;
using BulkyBook_Web.Models.Dto;

namespace BulkyBook_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CategoryDTO, CategoryCreateDTO>().ReverseMap();
            CreateMap<CategoryDTO, CategoryUpdateDTO>().ReverseMap();

            CreateMap<ProductDTO, ProductCreateDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductUpdateDTO>().ReverseMap();

            CreateMap<ShoppingCartDTO, ShoppingCartCreateDTO>().ReverseMap();
            CreateMap<ShoppingCartDTO, ShoppingCartUpdateDTO>().ReverseMap();

            CreateMap<OrderHeaderDTO, OrderHeaderCreateDTO>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeaderUpdateDTO>().ReverseMap();

            CreateMap<OrderDetailDTO, OrderDetailCreateDTO>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetailUpdateDTO>().ReverseMap();
        }
    }
}
