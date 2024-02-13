namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludePlaceVM
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
    }
}
