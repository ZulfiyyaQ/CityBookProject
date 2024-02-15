using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemHomeReviewVM
    {
        public int Id { get; init; }
        public string? Text { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
        public float RatingStar { get; init; }

        public IFormFile? Photo { get; init; }

        public string Surname { get; init; }
        public string Position { get; init; }
    }
}
