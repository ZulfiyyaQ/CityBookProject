using AutoMapper;
using CityBookMVCOnionApplication.ViewModels.BlogImage;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class BlogImageProfile : Profile
    {
        public BlogImageProfile()
        {
            CreateMap<IncludeBlogImageVM, BlogImage>().ReverseMap();
        }
    }
}
