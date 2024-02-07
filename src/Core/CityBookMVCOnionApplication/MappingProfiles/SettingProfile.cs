using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.Setting;
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
