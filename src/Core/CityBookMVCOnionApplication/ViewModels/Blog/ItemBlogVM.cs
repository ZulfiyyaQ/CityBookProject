namespace CityBookMVCOnionApplication.ViewModels
{

    public record ItemBlogVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateTime CreateAt { get; init; }
        public string UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public string Text { get; init; }
        public List<IncludeBlogImageVM>? BlogImage { get; init; }
        public List<IncludeBTagVM> Tags { get; init; }

    }
}
