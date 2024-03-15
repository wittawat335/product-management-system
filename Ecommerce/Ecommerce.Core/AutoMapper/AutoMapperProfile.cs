using AutoMapper;
using Ecommerce.Core.DTOs.Category;
using Ecommerce.Core.DTOs.Authen;
using Ecommerce.Core.DTOs.Product;
using Ecommerce.Domain.Entities;
using System.Globalization;
using Ecommerce.Core.DTOs;

namespace Ecommerce.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Auth
            CreateMap<User, LoginResponse>()
               .ForMember(x => x.positionName, opt => opt.MapFrom(origin => origin.Position.PositionName));
            CreateMap<PositionRequest, Position>();
            CreateMap<RegisterRequest, User>();
            #endregion

            #region User
            CreateMap<User, UserDTO>()
             .ForMember(x => x.PositionName, opt => opt.MapFrom(origin => origin.Position.PositionName))
             .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<UserDTO, User>()
               .ForMember(x => x.Position, opt => opt.Ignore())
               .ForMember(x => x.UserId, opt => opt.MapFrom(origin => new Guid(origin.UserId)))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            #endregion

            #region Product
            CreateMap<Product, ProductDTO>() //output
             .ForMember(x => x.CategoryName, opt => opt.MapFrom(origin => origin.Category.CategoryName))
             .ForMember(x => x.Stock, opt => opt.MapFrom(origin => String.Format($"{origin.Stock:n}")))
             .ForMember(x => x.Price, opt => opt.MapFrom(origin => String.Format($"{origin.Price:n}")))
             .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<ProductDTO, Product>() //input
               .ForMember(x => x.Category, opt => opt.Ignore())
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
               .ForMember(x => x.Stock, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Stock, new CultureInfo("en-US"))))
               .ForMember(x => x.Price, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US"))));
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>()
            .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<CategoryDTO, Category>().ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            #endregion

            #region Position

            CreateMap<PermissionDTO, Permission>().ReverseMap();

            CreateMap<Position, PositionDTO>()
               .ForMember(x => x.MenuDefaultName, opt => opt.MapFrom(origin => origin.MenuDefaultNavigation.MenuName))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<PositionDTO, Position>()
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            #endregion

            CreateMap<Menu, MenuDTO>().ReverseMap();
        }
    }
}
