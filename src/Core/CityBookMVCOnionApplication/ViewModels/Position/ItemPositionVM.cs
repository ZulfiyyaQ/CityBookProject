
using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Position
{
    public record ItemPositionVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludeUserVM>? User { get; init; }
    }
}
