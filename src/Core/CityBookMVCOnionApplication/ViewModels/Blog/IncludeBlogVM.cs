namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeBlogVM
    {
        public string Text { get; init; }
        public string Name { get; init; }
        public List<IncludeBlogImageVM> Images { get; set; }
        public List<string> ImageIds { get; init; }
        


    }



}
