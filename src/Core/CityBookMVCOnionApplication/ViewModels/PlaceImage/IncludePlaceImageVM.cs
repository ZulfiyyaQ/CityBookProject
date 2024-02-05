namespace CityBookMVCOnionApplication.ViewModels.PlaceImage
{
    public record IncludePlaceImageVM
    {
        public string ImageUrl { get; init; }
        public int Order { get; init; }
        public int PlaceId { get; init; }
    }
}
