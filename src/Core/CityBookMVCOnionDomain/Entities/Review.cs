namespace CityBookMVCOnionDomain.Entities
{
    public class Review : BaseNameableEntity
    {
        public string? Text { get; set; }
        public int RatingStar { get; set; }
        //Relational props
        public string UserId { get; set; }
       
        public User User { get; set; }
        public int PlaceId { get; set; }

        public Place Place { get; set; }
    }
}
