namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemReplyVM
    {
        public int Id { get; init; }
        public string Text { get; init; }

        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }

        public int CommentId { get; init; }
        public IncludeCommentVM Comment { get; init; }
    }
}
