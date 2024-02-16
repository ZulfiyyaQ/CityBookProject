namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeReviewVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Text { get; init; }
        public float RatingStar { get; init; }
    }
}
