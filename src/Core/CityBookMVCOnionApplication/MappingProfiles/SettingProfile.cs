﻿using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<UpdateSettingVM, Setting>().ReverseMap();
        }
    }
}
