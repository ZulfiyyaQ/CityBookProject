namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetCommentVM
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public DateTime CreateAt { get; init; }
        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }
        public int BlogId { get; init; }
        public IncludeBlogVM Blog { get; init; }
        public List<IncludeReplyVM> Replies { get; init; }
    }
}
