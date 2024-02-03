namespace CityBookMVCOnionDomain.Entities
{
    public class Position : BaseNameableEntity
    { //Relational props
        public List<Employee>? Employees { get; set; }
    }
}
