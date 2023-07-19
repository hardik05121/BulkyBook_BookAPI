using AutoMapper;
using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;

namespace MagicCategory_CategoryAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();

            CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();            CreateMap<ShoppingCart, ShoppingCartCreateDTO>().ReverseMap();            CreateMap<ShoppingCart, ShoppingCartUpdateDTO>().ReverseMap();

            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderUpdateDTO>().ReverseMap();
          
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreateDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateDTO>().ReverseMap();
        }
    }
}
