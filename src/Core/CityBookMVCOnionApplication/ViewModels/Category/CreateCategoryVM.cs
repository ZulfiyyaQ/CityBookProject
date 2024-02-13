using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record CreateCategoryVM
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public IFormFile Photo { get; init; }
        

    }
}
