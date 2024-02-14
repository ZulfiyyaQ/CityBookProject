namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetPlaceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string DayOrMonth { get; init; }

        public TimeSpan? Monday { get; init; }
        public TimeSpan? Tuesday { get; init; }
        public TimeSpan? Wednesday { get; init; }
        public TimeSpan? Thursday { get; init; }
        public TimeSpan? Friday { get; init; }
        public TimeSpan? Saturday { get; init; }
        public TimeSpan? Sunday { get; init; }
        public TimeSpan? MondayTo { get; init; }
        public TimeSpan? TuesdayTo { get; init; }
        public TimeSpan? WednesdayTo { get; init; }
        public TimeSpan? ThursdayTo { get; init; }
        public TimeSpan? FridayTo { get; init; }
        public TimeSpan? SaturdayTo { get; init; }
        public TimeSpan? SundayTo { get; init; }

        public string Address { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public int CategoryId { get; init; }
        public IncludeCategoryVM Category { get; init; }
        public List<IncludePlaceImageVM> PlaceImages { get; init; }
        public List<IncludeTagVM> Tags { get; init; }
        public List<IncludeFeatureVM> Features { get; init; }
        public IncludeReviewVM Review { get; init; }
    }
}
