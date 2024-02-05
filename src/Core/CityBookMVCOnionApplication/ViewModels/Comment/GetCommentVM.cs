using CityBookMVCOnionApplication.ViewModels.Blog;
using CityBookMVCOnionApplication.ViewModels.Reply;
using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Comment
{
    public record GetCommentVM
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public int BlogId { get; init; }
        public IncludeBlogVM Blog { get; init; }
        public List<IncludeReplyVM> Replies { get; init; }
    }
}
