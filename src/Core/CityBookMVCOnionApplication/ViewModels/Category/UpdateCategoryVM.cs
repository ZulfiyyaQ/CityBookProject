using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record UpdateCategoryVM
    {
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public IFormFile? Photo { get; init; }
        
    }
}
