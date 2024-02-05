namespace CityBookMVCOnionApplication.ViewModels.Service
{
    public record UpdateServiceVM
    {
        public string Name { get; init; }
        public string Icon { get; init; }
        public string Description { get; init; }
    }
}
