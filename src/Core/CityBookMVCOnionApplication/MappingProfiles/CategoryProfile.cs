using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.BTag;
using CityBookMVCOnionApplication.ViewModels.Category;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryVM, Category>().ReverseMap();
            CreateMap<UpdateCategoryVM, Category>().ReverseMap();
            CreateMap<GetCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.Places.ToList()));
            CreateMap<IncludeCategoryVM, Category>().ReverseMap();
            CreateMap<ItemCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.Places.ToList()));
        }
    }
}
