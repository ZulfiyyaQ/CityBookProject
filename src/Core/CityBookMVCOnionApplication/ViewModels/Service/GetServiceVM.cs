namespace CityBookMVCOnionApplication.ViewModels.Service
{
    public record GetServiceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Icon { get; init; }
        public string Description { get; init; }
    }
}
