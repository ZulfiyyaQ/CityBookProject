using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.Blog;
using CityBookMVCOnionApplication.ViewModels.Employee;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeVM, Employee>().ReverseMap();
            CreateMap<GetEmployeeVM, Employee>().ReverseMap()
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position));

            CreateMap<UpdateEmployeeVM, Employee>().ReverseMap();
                
            CreateMap<ItemEmployeeVM, Employee>().ReverseMap()
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position));
            CreateMap<IncludeEmployeeVM, Employee>().ReverseMap();
                
        }
    }
}
