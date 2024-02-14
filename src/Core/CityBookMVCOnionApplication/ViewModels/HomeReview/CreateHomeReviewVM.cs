using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record CreateHomeReviewVM
    {
        public string? Text { get; init; }
        public string Name { get; init; }
        public int RatingStar { get; init; }
        
        public IFormFile Photo { get; init; }

        public string Surname { get; init; }
        public string Position { get; init; }
        
    }
}
