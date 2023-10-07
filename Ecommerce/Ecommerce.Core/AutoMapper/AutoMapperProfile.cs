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

            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(origin => origin.Category.CategoryName))
                .ForMember(x => x.Price, opt => opt.MapFrom(origin => String.Format($"{origin.Price:n}")))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            CreateMap<ProductDTO, Product>()
               .ForMember(x => x.Category, opt => opt.Ignore())
               .ForMember(x => x.ProductId, opt => opt.MapFrom(origin => new Guid(origin.ProductId)))
               .ForMember(x => x.CategoryId, opt => opt.MapFrom(origin => new Guid(origin.CategoryId)))
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
               .ForMember(x => x.Price, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US"))));

            CreateMap<Category, CategoryDTO>()
               .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
            //CreateMap<CategoryDTO, Category>()
            //   .ForMember(x => x.Category, opt => opt.Ignore())
            //   .ForMember(x => x.ProductId, opt => opt.MapFrom(origin => new Guid(origin.ProductId)))
            //   .ForMember(x => x.CategoryId, opt => opt.MapFrom(origin => new Guid(origin.CategoryId)))
            //   .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
            //   .ForMember(x => x.Price, opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US"))));
        }
    }


    //.ForMember(x => x.Image, opt =>
    //opt.MapFrom(origin => origin.Image != null
    //? String.Format("http://localhost:5133/images/product/{0}/{0}.png", origin.ProductId)
    //: "http://localhost:5133/images/no_photo.jpg"))
    //+ '<img src="~/images/product/' + row.image + '" alt="' + row.productName + '" width="50" height="50" />'
}
