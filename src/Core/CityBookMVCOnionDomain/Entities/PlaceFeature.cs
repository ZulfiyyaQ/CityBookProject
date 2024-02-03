namespace CityBookMVCOnionDomain.Entities
{
    public class PlaceFeature : BaseEntity
    { //Relational props
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
