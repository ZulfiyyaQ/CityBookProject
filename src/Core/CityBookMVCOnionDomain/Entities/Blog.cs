namespace CityBookMVCOnionDomain.Entities
{
    public class Blog : BaseNameableEntity
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public DateTime PublishedTime { get; set; }
        //Relational props
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
