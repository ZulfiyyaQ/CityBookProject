namespace CityBookMVCOnionDomain.Entities
{
    public class Position : BaseNameableEntity
    { //Relational props
        public List<User>? Users { get; set; }
    }
}
