using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{

    public record UpdateBlogVM
    {
        public List<IFormFile>? Photos { get; init; }
        
        public string Name { get; init; }
        public List<IncludeBTagVM> Tags { get; set; }
        public List<int> TagIds { get; set; }
        public string Text { get; init; }
        public ICollection<IncludePlaceImageVM>? Images { get; init; }
        public List<int>? ImageIds { get; set; }
    }
}
