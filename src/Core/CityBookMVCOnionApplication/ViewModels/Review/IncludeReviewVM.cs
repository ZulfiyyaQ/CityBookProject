namespace CityBookMVCOnionApplication.ViewModels.Review
{
    public record IncludeReviewVM
    {
        public string? Text { get; init; }
        public float RatingStar { get; init; }
    }
}
