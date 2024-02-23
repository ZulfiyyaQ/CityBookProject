using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            CreateMap<CreateReplyVM, Reply>().ReverseMap();
            CreateMap<UpdateReplyVM, Reply>().ReverseMap();
            CreateMap<GetReplyVM, Reply>().ReverseMap()
                .ForMember(x => x.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
            CreateMap<IncludeReplyVM, Reply>().ReverseMap()
                .ForMember(x => x.ReplyUser, opt => opt.MapFrom(src => src.User));
            CreateMap<ItemReplyVM, Reply>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
