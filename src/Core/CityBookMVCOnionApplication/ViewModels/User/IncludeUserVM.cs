using CityBookMVCOnionApplication.ViewModels.Position;

namespace CityBookMVCOnionApplication.ViewModels.User
{
    public record IncludeUserVM
    {
        public string? Image { get; init; }
        public int PositionId { get; set; }
        public IncludePositionVM Position { get; set; }

    }
}
