using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.HomeReview;
using CityBookMVCOnionApplication.ViewModels.Service;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class HomeReviewProfile : Profile
    {
        public HomeReviewProfile()
        {
            CreateMap<CreateHomeReviewVM, HomeReview>().ReverseMap();
            CreateMap<UpdateHomeReviewVM, HomeReview>().ReverseMap();
            CreateMap<GetHomeReviewVM, HomeReview>().ReverseMap();
            CreateMap<ItemHomeReviewVM, HomeReview>().ReverseMap();
        }
    }
}
