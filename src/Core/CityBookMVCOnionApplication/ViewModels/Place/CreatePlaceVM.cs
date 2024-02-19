using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record CreatePlaceVM
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string DayOrMonth { get; init; }
        public List<IFormFile> Photos { get; init; }

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

        public List<IncludeTagVM>? Tags { get; set; }
        public List<IncludeFeatureVM>? Features { get; set; }
        public List<IncludeCategoryVM>? Categories { get; set; }

        public int CategoryId { get; init; }
        public List<int> TagIds { get; init; }
        public List<int> FeatureIds { get; init; }

        
        
    }
}
