namespace CityBookMVCOnionDomain.Entities
{
    public class BlogTag : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int TagId { get; set; }
        public BTag Tag { get; set; }
    }
}
