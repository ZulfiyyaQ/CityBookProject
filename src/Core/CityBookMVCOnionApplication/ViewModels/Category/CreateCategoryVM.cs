using CityBookMVCOnionApplication.ViewModels.Blog;
using CityBookMVCOnionApplication.ViewModels.Place;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Category
{
    public record CreateCategoryVM
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public IFormFile Photo { get; init; }
        public List<IncludePlaceVM> Places { get; init; }

    }
}
