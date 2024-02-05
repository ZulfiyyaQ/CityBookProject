using CityBookMVCOnionApplication.ViewModels.Place;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Category
{
    public record GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl{ get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
