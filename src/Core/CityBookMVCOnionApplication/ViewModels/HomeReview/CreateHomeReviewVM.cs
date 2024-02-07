using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.HomeReview
{
    public record CreateHomeReviewVM
    {
        public string? Text { get; init; }
        public string Name { get; init; }
        public float RatingStar { get; init; }
        
        public IFormFile Photo { get; init; }

        public string Surname { get; init; }
        public string Position { get; init; }
        
    }
}
