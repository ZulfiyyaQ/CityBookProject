namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetReviewVM
    {
        public int Id { get; init; }
        public string? Text { get; init; }
        public string Name { get; init; }
        public float RatingStar { get; init; }

        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public int PlaceId { get; init; }
        public IncludePlaceVM Place { get; init; }
    }
}
