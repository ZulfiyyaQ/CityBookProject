using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
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
