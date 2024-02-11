using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.Position;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionVM, Position>().ReverseMap();
            CreateMap<UpdatePositionVM, Position>().ReverseMap();
            CreateMap<GetPositionVM, Position>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.Users.ToList()));
            CreateMap<IncludePositionVM, Position>().ReverseMap();
            CreateMap<ItemPositionVM, Position>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.Users.ToList()));
        }
    }
}
