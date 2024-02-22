using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludePlaceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string Website { get; init; }
        public List<IncludePlaceImageVM>? PlaceImages { get; init; }
        public IncludeUserVM? User { get; init; }
        public IncludeCategoryVM? Category { get; init; }
        public List<IncludeReviewVM>? Review { get; init; }
    }
}
