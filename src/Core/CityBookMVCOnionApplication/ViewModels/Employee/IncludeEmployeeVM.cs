namespace CityBookMVCOnionApplication.ViewModels.Employee
{
    public record IncludeEmployeeVM
    {
        public string Surname { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public string Face { get; init; }
        public string Tvit { get; init; }
        public string Link { get; init; }
    }
}
