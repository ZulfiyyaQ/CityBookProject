namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemPlaceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public int CategoryId { get; init; }
        public IncludeCategoryVM Category { get; init; }
        public List<IncludePlaceImageVM> PlaceImages { get; init; }
        public List<IncludeTagVM> Tags { get; init; }
        public List<IncludeFeatureVM> Features { get; init; }
        public IncludeReviewVM Review { get; init; }
    }
}
