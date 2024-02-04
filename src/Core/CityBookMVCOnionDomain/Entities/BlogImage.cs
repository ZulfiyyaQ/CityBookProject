namespace CityBookMVCOnionDomain.Entities
{
    public class BlogImage:BaseEntity
    {
        public string Url { get; set; } = null!;
        
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
    }
}
