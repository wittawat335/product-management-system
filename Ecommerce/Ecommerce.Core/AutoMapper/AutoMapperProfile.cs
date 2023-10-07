using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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
                .ForMember(x => x.CreateDate,
                opt => opt.MapFrom(origin => String.Format("{0:dd-MM-yyyy}", origin.CreateDate)));
        }
    }


    //.ForMember(x => x.Image, opt =>
    //opt.MapFrom(origin => origin.Image != null
    //? String.Format("http://localhost:5133/images/product/{0}/{0}.png", origin.ProductId)
    //: "http://localhost:5133/images/no_photo.jpg"))
}
