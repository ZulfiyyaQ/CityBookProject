using CityBookMVCOnionApplication.ViewModels.PlaceImage;
using CityBookMVCOnionApplication.ViewModels.Review;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Place
{
    public record UpdatePlaceVM
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public List<IFormFile>? Photos { get; init; }
        public ICollection<IncludePlaceImageVM>? Images { get; set; }
        public List<int>? ImageIds { get; set; }
        public List<IncludeReviewVM>? Reviews { get; init; }
        public int CategoryId { get; init; }
        public int UserId { get; init; }
        public List<int> TagIds { get; init; }
        public List<int> FeatureIds { get; init; }
    }
}
