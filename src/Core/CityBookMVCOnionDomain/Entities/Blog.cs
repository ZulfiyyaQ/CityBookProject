namespace CityBookMVCOnionDomain.Entities
{
    public class Blog : BaseNameableEntity
    {
        
        public string Text { get; set; }

        //Relational props
        public List<BlogImage> BlogImages { get; set; } = null!;
        public string UserId { get; set; }
        public User User { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
