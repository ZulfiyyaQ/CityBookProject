namespace CityBookMVCOnionDomain.Entities
{
    public class PlaceImage:BaseEntity
    {
        public string ImageUrl { get; set; } = null!;

        public int Order { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
    }
}
