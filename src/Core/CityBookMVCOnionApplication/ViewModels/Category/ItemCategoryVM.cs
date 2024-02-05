using CityBookMVCOnionApplication.ViewModels.Place;

namespace CityBookMVCOnionApplication.ViewModels.Category
{
    public record ItemCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
