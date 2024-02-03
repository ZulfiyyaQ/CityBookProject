namespace CityBookMVCOnionDomain.Entities
{
    public class Tag : BaseNameableEntity
    {//Relational props
        public List<PlaceTag> PlaceTags { get; set; }
    }
}
