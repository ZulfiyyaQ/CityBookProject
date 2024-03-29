﻿namespace CityBookMVCOnionApplication.ViewModels
{
    public record UpdateReviewVM
    {
        public string? Text { get; init; }
        public string Name { get; init; }
        public int RatingStar { get; init; }

        public int UserId { get; init; }
        public int PlaceId { get; init; }
    }
}
