using AutoMapper;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.MappingProfiles
{
    internal class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentVM, Comment>().ReverseMap();
            CreateMap<UpdateCommentVM, Comment>().ReverseMap();
            CreateMap<GetCommentVM, Comment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.Replies.ToList()))
                .ForMember(x => x.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
            CreateMap<IncludeCommentVM, Comment>().ReverseMap();
                
                
            CreateMap<ItemCommentVM, Comment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.Replies.ToList()))
                .ForMember(x => x.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
