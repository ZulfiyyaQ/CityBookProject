namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetBTagVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludeBlogVM> Blogs { get; init; }
    }
}
