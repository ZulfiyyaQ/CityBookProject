using CityBookMVCOnionApplication.ViewModels.Employee;

namespace CityBookMVCOnionApplication.ViewModels.Position
{
    public record GetPositionVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludeEmployeeVM>? Employees { get; init; }
    }
}
