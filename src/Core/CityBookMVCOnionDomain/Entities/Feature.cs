namespace CityBookMVCOnionDomain.Entities
{
    public class Feature : BaseNameableEntity
    {
        public string Icon { get; set; }
        //Relational props
        public List<PlaceFeature>? PlaceFeature { get; set; }
    }
}
