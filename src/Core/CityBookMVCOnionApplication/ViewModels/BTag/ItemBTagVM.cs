﻿using CityBookMVCOnionApplication.ViewModels.Blog;

namespace CityBookMVCOnionApplication.ViewModels.BTag
{
    public record ItemBTagVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludeBlogVM> Blogs { get; init; }
    }
}
