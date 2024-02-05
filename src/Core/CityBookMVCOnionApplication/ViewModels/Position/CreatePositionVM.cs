using CityBookMVCOnionApplication.ViewModels.Employee;

namespace CityBookMVCOnionApplication.ViewModels.Position
{
    public record CreatePositionVM
    {
        public string Name { get; init; }
        public List<IncludeEmployeeVM>? Employees { get; init; }
    }
}
