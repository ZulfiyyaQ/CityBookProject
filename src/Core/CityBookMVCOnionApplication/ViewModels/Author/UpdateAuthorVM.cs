using CityBookMVCOnionApplication.ViewModels.Blog;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Author
{
    public record UpdateAuthorVM
    {
        public IFormFile Photo { get; init; }
        public string Description { get; init; }
        public string Name { get; init; }
        public string Tvit { get; init; }
        public string VK { get; init; }
        public string Wp { get; init; }
        public string Inst { get; init; }
        public string Phone { get; init; }
        public string Address { get; init; }
        public string Mail { get; init; }
        public string Website { get; init; }
        public string ImageUrl { get; init; }
        public List<IncludeBlogVM> Blogs { get; init; }
    }

}
