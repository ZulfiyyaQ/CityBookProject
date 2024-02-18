using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeReviewVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Text { get; init; }
        public int RatingStar { get; init; }
        public string UserId { get; init; }
        public IncludeUserVM User { get; init; }
    }
}
