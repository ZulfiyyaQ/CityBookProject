using CityBookMVCOnionApplication.ViewModels.BlogImage;
using CityBookMVCOnionApplication.ViewModels.BTag;
using CityBookMVCOnionApplication.ViewModels.Comment;
using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Blog
{

    public record GetBlogVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public string Text { get; init; }
        public List<IncludeBlogImageVM> BlogImage { get; init; }
        public List<IncludeBTagVM> Tags { get; init; }
        public List<IncludeCommentVM>? Comments { get; init; }

    }
}
