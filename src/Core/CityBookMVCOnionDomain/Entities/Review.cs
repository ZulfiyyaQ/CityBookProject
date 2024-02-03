namespace CityBookMVCOnionDomain.Entities
{
    public class Review : BaseEntity
    {
        public string? Text { get; set; }
        public float RatingStar { get; set; }
        //Relational props
        public int UserId { get; set; }
       
        public User User { get; set; }
    }
}
