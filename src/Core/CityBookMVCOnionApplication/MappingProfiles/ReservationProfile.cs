using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationVM, Reservation>().ReverseMap();

        }
    }
}
