namespace CityBookMVCOnionDomain.Entities
{
    public class Comment : BaseNameableEntity
    {
        public string Text { get; set; }
        
        //Relational props
        public string UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public List<Reply>? Replies { get; set; }
    }
}
