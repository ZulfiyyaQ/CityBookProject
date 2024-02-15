namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemCategoryVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
