namespace CityBookMVCOnionDomain.Entities
{
    public class Place : BaseNameableEntity
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public TimeSpan? Monday { get; set; }
        public TimeSpan? Tuesday { get; set; }
        public TimeSpan? Wednesday { get; set; }
        public TimeSpan? Thursday { get; set; }
        public TimeSpan? Friday { get; set; }
        public TimeSpan? Saturday { get; set; }
        public TimeSpan? Sunday { get; set; }
        public TimeSpan? MondayTo { get; set; }
        public TimeSpan? TuesdayTo { get; set; }
        public TimeSpan? WednesdayTo { get; set; }
        public TimeSpan? ThursdayTo { get; set; }
        public TimeSpan? FridayTo { get; set; }
        public TimeSpan? SaturdayTo { get; set; }
        public TimeSpan? SundayTo { get; set; }

        public string DayOrMonth { get; set; } = null!;

        //Relational props
        public List<Review>? Reviews { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public List<PlaceTag> PlaceTags { get; set; }
        public List<PlaceFeature> PlaceFeatures { get; set; }
        public List<PlaceImage> PlaceImages { get; set; }
        public List<Reservation>? Reservations { get; set; }


    }
}
