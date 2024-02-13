namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemBTagVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludeBlogVM> Blogs { get; init; }
    }
}
