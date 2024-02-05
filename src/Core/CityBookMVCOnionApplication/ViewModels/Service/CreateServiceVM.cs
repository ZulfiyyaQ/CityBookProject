namespace CityBookMVCOnionApplication.ViewModels.Service
{
    public record CreateServiceVM
    {
        public string Name { get; init; }
        public string Icon { get; init; }
        public string Description { get; init; }
    }
}
