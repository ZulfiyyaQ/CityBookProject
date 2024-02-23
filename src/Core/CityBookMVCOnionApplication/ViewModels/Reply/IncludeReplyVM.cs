namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeReplyVM
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public DateTime CreateAt { get; init; }
        public IncludeUserVM?  ReplyUser { get; init; }

    }
}
