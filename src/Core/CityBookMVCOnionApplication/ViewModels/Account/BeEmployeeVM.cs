using CityBookMVCOnionApplication.ViewModels.Position;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record BeEmployeeVM
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string? Image { get; init; }
        public List<IncludePositionVM> Positions { get; set; }
        public int PositionId { get; set; }
    }
}
