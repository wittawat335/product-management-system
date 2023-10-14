using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.DTOs.Authen;
using Ecommerce.Domain.Entities;
using System.Globalization;

namespace Ecommerce.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PositionRequest, Position>();
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.PositionId, opt => opt.MapFrom(origin => new Guid(origin.positionId)));
            CreateMap<User, UserPosition>();

            CreateMap<User, UserDTO>()
                .ForMember(x => x.PositionName, opt => opt.MapFrom(origin => origin.Position.PositionName))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<UserDTO, User>()
               .ForMember(x => x.Position, opt => opt.Ignore())
               .ForMember(x => x.UserId, opt => opt.MapFrom(origin => new Guid(origin.UserId)))
               .ForMember(x => x.PositionId, opt => opt.MapFrom(origin => new Guid(origin.PositionId)))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));

            CreateMap<Menu, MenuDTO>(); //output
            CreateMap<MenuDTO, Menu>() //input
                .ForMember(x => x.MenuId, opt => opt.MapFrom(origin => new Guid(origin.MenuId)))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(origin => new Guid(origin.ParentId)));

            CreateMap<Product, ProductDTO>() //output
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(origin => origin.Category.CategoryName))
                .ForMember(x => x.Stock, opt => opt.MapFrom(origin => String.Format($"{origin.Stock:n}")))
                .ForMember(x => x.Price, opt => opt.MapFrom(origin => String.Format($"{origin.Price:n}")))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<ProductDTO, Product>() //input
               .ForMember(x => x.Category, opt => opt.Ignore())
               .ForMember(x => x.ProductId, opt => opt.MapFrom(origin => new Guid(origin.ProductId)))
               .ForMember(x => x.CategoryId, opt => opt.MapFrom(origin => new Guid(origin.CategoryId)))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
               .ForMember(x => x.Stock, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Stock, new CultureInfo("en-US"))))
               .ForMember(x => x.Price, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US"))));

            CreateMap<Category, CategoryDTO>()
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<CategoryDTO, Category>()
               .ForMember(x => x.CategoryId, opt => opt.MapFrom(origin => new Guid(origin.CategoryId)))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
        }
    }
}
