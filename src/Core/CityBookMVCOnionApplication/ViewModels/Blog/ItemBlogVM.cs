using CityBookMVCOnionApplication.ViewModels.BlogImage;
using CityBookMVCOnionApplication.ViewModels.BTag;

namespace CityBookMVCOnionApplication.ViewModels.Blog
{
    
    public record ItemBlogVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int UserId { get; init; }
        public string Text { get; init; }
        public List<IncludeBlogImageVM> BlogImage { get; init; }
        public List<IncludeBTagVM> Tags { get; init; }

    }
}
