using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.Feature;
using CityBookMVCOnionApplication.ViewModels.Tag;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<CreateFeatureVM, Feature>().ReverseMap();
            CreateMap<UpdateFeatureVM, Feature>().ReverseMap();
            CreateMap<GetFeatureVM, Feature>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.PlaceFeature.Select(x => x.Place).ToList()));
            CreateMap<IncludeFeatureVM, Feature>().ReverseMap();
            CreateMap<ItemFeatureVM, Feature>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.PlaceFeature.Select(x => x.Place).ToList()));
        }
    }
}
