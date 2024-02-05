namespace CityBookMVCOnionDomain.Entities
{
    public class Place : BaseNameableEntity
    {
        public string Address { get; set; }
        public string Description { get; set; }
        
        //Relational props
        public List<Review>? Reviews { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<PlaceTag> PlaceTags { get; set; }
        public List<PlaceFeature> PlaceFeatures { get; set; }
        public List<PlaceImage> PlaceImages { get; set; }

    }
}
