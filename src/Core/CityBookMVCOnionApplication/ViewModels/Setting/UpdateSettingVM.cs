namespace CityBookMVCOnionApplication.ViewModels.Setting
{
    public record UpdateSettingVM
    {
        public string Key { get; init; }
        public string Value { get; init; }
    }
}
