namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludePlaceImageVM
    {
        public int Id { get; init; }
        public string ImageUrl { get; init; }
        public int Order { get; init; }
        public int PlaceId { get; init; }
    }
}
