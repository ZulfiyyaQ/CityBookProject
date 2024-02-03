namespace CityBookMVCOnionDomain.Entities
{
    public class PlaceTag : BaseEntity
    { //Relational props
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
