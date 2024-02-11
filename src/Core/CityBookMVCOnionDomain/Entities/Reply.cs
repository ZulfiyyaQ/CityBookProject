namespace CityBookMVCOnionDomain.Entities
{
    public class Reply : BaseEntity
    {
        public string Text { get; set; }
        //Relational props
        public string UserId { get; set; }
        public User User { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
