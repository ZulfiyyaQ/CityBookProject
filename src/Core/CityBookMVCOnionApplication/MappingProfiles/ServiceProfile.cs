using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceVM, Service>().ReverseMap();
            CreateMap<UpdateServiceVM, Service>().ReverseMap();
            CreateMap<GetServiceVM, Service>().ReverseMap();
            CreateMap<ItemServiceVM, Service>().ReverseMap();

        }
    }
}
