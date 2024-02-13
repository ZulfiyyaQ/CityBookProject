using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{

    public record UpdateBlogVM
    {
        public List<IFormFile>? Photos { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public string Name { get; init; }
        public List<IncludeTagVM> Tags { get; set; }
        public List<int> TagIds { get; init; }
        public string Text { get; init; }
        public ICollection<IncludePlaceImageVM>? Images { get; init; }
        public List<int>? ImageIds { get; set; }
    }
}
