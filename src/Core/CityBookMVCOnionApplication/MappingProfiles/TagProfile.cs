using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.Tag;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagVM, Tag>().ReverseMap();
            CreateMap<UpdateTagVM, Tag>().ReverseMap();
            CreateMap<GetTagVM, Tag>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.PlaceTags.Select(x => x.Place).ToList()));
            CreateMap<IncludeTagVM, Tag>().ReverseMap();
            CreateMap<ItemTagVM, Tag>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.PlaceTags.Select(x => x.Place).ToList()));
        }
    }
}
