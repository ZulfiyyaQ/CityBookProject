using AutoMapper;
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
            CreateMap<ItemUserVM, User>().ReverseMap();
            CreateMap<IncludeUserVM, User>().ReverseMap();
            CreateMap<LoginVM, User>().ReverseMap();
            CreateMap<RegisterVM, User>().ReverseMap();
            CreateMap<GetUserVM, User>().ReverseMap()
                .ForMember(x => x.Blogs, opt => opt.MapFrom(src => src.Blogs.ToList()))
                .ForMember(x => x.Reviews, opt => opt.MapFrom(src => src.Reviews.ToList()))
                .ForMember(x => x.Places, opt => opt.MapFrom(src => src.Places.ToList()))
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.Replies.ToList()));


        }
    }
}
