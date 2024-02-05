using CityBookMVCOnionApplication.ViewModels.Place;

namespace CityBookMVCOnionApplication.ViewModels.Tag
{
    public record ItemTagVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
