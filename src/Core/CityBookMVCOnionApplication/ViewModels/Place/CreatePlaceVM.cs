using CityBookMVCOnionApplication.ViewModels.Review;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Place
{
    public record CreatePlaceVM
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public List<IFormFile> Photos { get; init; }

        public List<IncludeReviewVM>? Reviews { get; init; }
        public int CategoryId { get; init; }
        public string UserId { get; init; }
        public List<int> TagIds { get; init; }
        public List<int> FeatureIds { get; init; }

        
        
    }
}
