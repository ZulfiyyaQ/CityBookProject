namespace CityBookMVCOnionDomain.Entities
{
    public class Blog : BaseNameableEntity
    {
        
        public string Text { get; set; }

        //Relational props
        public List<BlogImage> BlogImages { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
