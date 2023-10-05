using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Domain.Entities;

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
                .ForMember(x => x.Image, opt => opt.MapFrom(origin => String.Format("http://localhost:5133/Upload/product/{0}/{0}.png", origin.ProductId)))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
        }
    }
}
