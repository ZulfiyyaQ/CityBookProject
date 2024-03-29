﻿using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewVM, Review>().ReverseMap();
            CreateMap<UpdateReviewVM, Review>().ReverseMap();
            CreateMap<GetReviewVM, Review>().ReverseMap()
                
                .ForMember(x => x.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
            CreateMap<IncludeReviewVM, Review>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));


            CreateMap<ItemReviewVM, Review>().ReverseMap()
                
                .ForMember(x => x.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
