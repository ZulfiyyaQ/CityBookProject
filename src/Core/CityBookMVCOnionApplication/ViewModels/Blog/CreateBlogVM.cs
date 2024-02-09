﻿using CityBookMVCOnionApplication.ViewModels.Tag;
using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Blog
{
    public record CreateBlogVM
    {
        public List<IFormFile> Photos { get; init; }
        public string Name { get; init; }
        public List<IncludeTagVM> Tags { get; set; }
        public List<int> TagIds { get; init; }
        public string Text { get; init; }
        public int UserId { get; init; }
        

    }
}