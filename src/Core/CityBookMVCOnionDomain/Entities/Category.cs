namespace CityBookMVCOnionDomain.Entities
{
    public class Category : BaseNameableEntity
    {
        public string Description { get; set; }
        public string Image { get; set; }
        //Relational props
        public List<Place>? Places { get; set; }
    }
}
