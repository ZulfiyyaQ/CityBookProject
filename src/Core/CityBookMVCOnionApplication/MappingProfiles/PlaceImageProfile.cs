using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.PlaceImage;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class PlaceImageProfile : Profile
    {
        public PlaceImageProfile()
        {
            CreateMap<IncludePlaceImageVM, PlaceImage>().ReverseMap();
        }
    }
}
