using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<CreatePlaceVM, Place>().ReverseMap();
            CreateMap<UpdatePlaceVM, Place>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.PlaceImages.ToList()));
            CreateMap<GetPlaceVM, Place>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(x => x.Review, opt => opt.MapFrom(src => src.Reviews.ToList()))
                .ForMember(x => x.PlaceImages, opt => opt.MapFrom(src => src.PlaceImages.ToList()))
                .ForMember(x => x.Tags, opt => opt.MapFrom(src => src.PlaceTags.Select(ma => ma.Tag).ToList()))
                .ForMember(x => x.Features, opt => opt.MapFrom(src => src.PlaceFeatures.Select(ma => ma.Feature).ToList()));
            CreateMap<IncludePlaceVM, Place>().ReverseMap()
                .ForMember(x => x.Review, opt => opt.MapFrom(src => src.Reviews.ToList()))
                .ForMember(x => x.Reservations, opt => opt.MapFrom(src => src.Reservations.ToList()))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(x => x.PlaceImages, opt => opt.MapFrom(src => src.PlaceImages.ToList()));
            CreateMap<ItemPlaceVM, Place>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.PlaceImages, opt => opt.MapFrom(src => src.PlaceImages.ToList()))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(x => x.Review, opt => opt.MapFrom(src => src.Reviews.ToList()))
                .ForMember(x => x.Tags, opt => opt.MapFrom(src => src.PlaceTags.Select(ma => ma.Tag).ToList()))
                .ForMember(x => x.Features, opt => opt.MapFrom(src => src.PlaceFeatures.Select(ma => ma.Feature).ToList()));


        }
    }
}
