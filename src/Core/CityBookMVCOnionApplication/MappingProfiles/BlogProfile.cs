using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<CreateBlogVM, Blog>().ReverseMap();
            CreateMap<GetBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.BlogImage, opt => opt.MapFrom(src => src.BlogImages.ToList()))
                .ForMember(x => x.Tags, opt => opt.MapFrom(src => src.BlogTags.Select(x => x.Tag).ToList()))
                .ForMember(x => x.Comments, opt => opt.MapFrom(src => src.Comments.ToList()));
            CreateMap<UpdateBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()));
            CreateMap<ItemBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Tags, opt => opt.MapFrom(src => src.BlogTags.Select(x => x.Tag).ToList()))
                .ForMember(x => x.BlogImage, opt => opt.MapFrom(src => src.BlogImages.ToList()));
            CreateMap<IncludeBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()));
        }
    }
}
