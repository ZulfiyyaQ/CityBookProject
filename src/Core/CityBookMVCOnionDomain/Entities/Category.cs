namespace CityBookMVCOnionDomain.Entities
{
    internal class Category : BaseNameableEntity
    {
        public ICollection<Place>? Places { get; set; }
    }
}
