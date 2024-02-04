namespace CityBookMVCOnionDomain.Entities
{
    public class HomeReview : BaseNameableEntity
    {
        public string Text { get; set; }
        public string Image { get; set; }
        
        public string Surname { get; set; }
        public string Position { get; set; }
        public float RatingStar { get; set; }
       
    }
}
