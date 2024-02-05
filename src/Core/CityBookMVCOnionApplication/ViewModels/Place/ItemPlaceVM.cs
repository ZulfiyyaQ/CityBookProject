using CityBookMVCOnionApplication.ViewModels.Feature;
using CityBookMVCOnionApplication.ViewModels.PlaceImage;
using CityBookMVCOnionApplication.ViewModels.Tag;
using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Place
{
    public record ItemPlaceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public List<IncludePlaceImageVM> PlaceImages { get; init; }
        public List<IncludeTagVM> Tags { get; init; }
        public List<IncludeFeatureVM> Features { get; init; }
    }
}
