using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.BTag;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class BTagProfile : Profile
    {
        public BTagProfile()
        {
            CreateMap<CreateBTagVM, BTag>().ReverseMap();
            CreateMap<UpdateBTagVM, BTag>().ReverseMap();
            CreateMap<GetBTagVM, BTag>().ReverseMap()
                .ForMember(x => x.Blogs, opt => opt.MapFrom(src => src.BlogTags.Select(x => x.Blog).ToList()));
            CreateMap<IncludeBTagVM, BTag>().ReverseMap();
            CreateMap<ItemBTagVM, BTag>().ReverseMap()
                .ForMember(x => x.Blogs, opt => opt.MapFrom(src => src.BlogTags.Select(x => x.Blog).ToList()));
        }
    }
}
