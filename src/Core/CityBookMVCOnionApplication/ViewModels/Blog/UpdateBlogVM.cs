using CityBookMVCOnionApplication.ViewModels.PlaceImage;
using Microsoft.AspNetCore.Http;
using a = CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.ViewModels.Blog
{

    public record UpdateBlogVM
    {
        public List<IFormFile>? Photos { get; init; }
        public int UserId { get; init; }
        public string Name { get; init; }
        public List<int> TagIds { get; init; }
        public string Text { get; init; }
        public ICollection<IncludePlaceImageVM>? Images { get; set; }
        public List<int>? ImageIds { get; set; }
    }
}
