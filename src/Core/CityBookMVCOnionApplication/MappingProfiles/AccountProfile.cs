﻿using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<EditUserVM, User>().ReverseMap();
            CreateMap<ChangePasswordVM, User>().ReverseMap();
            CreateMap<IncludeUserVM, User>().ReverseMap();
            CreateMap<LoginVM, User>().ReverseMap();
            CreateMap<RegisterVM, User>().ReverseMap();
            CreateMap<BeEmployeeVM, User>().ReverseMap();
        }
    }
}
