using CityBookMVCOnionApplication.ViewModels.Category;
using CityBookMVCOnionApplication.ViewModels.Feature;
using CityBookMVCOnionApplication.ViewModels.Review;
using CityBookMVCOnionApplication.ViewModels.Tag;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Place
{
    public record CreatePlaceVM
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public List<IFormFile> Photos { get; init; }

        public List<IncludeTagVM>? Tags { get; set; }
        public List<IncludeFeatureVM>? Features { get; set; }
        public List<IncludeCategoryVM>? Categories { get; set; }

        public int CategoryId { get; init; }
        public string UserId { get; init; }
        public List<int> TagIds { get; init; }
        public List<int> FeatureIds { get; init; }

        
        
    }
}
