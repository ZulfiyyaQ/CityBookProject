﻿namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetTagVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<IncludePlaceVM> Places { get; init; }
    }
}
