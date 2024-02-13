namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetFeatureVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Icon { get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
