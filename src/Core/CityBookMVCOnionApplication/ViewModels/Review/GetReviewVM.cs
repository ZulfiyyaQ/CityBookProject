using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Review
{
    public record GetReviewVM
    {
        public int Id { get; init; }
        public string? Text { get; init; }
        public float RatingStar { get; init; }

        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
    }
}
