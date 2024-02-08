using CityBookMVCOnionApplication.ViewModels.Account;
using CityBookMVCOnionApplication.ViewModels.PlaceImage;
using CityBookMVCOnionApplication.ViewModels.Tag;
using Microsoft.AspNetCore.Http;
using a = CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.ViewModels.Blog
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
