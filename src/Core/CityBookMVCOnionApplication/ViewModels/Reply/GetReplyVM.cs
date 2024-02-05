using CityBookMVCOnionApplication.ViewModels.Comment;
using CityBookMVCOnionApplication.ViewModels.User;

namespace CityBookMVCOnionApplication.ViewModels.Reply
{
    public record GetReplyVM
    {
        public int Id { get; init; }
        public string Text { get; init; }

        public int UserId { get; init; }
        public IncludeUserVM User { get; init; }

        public int CommentId { get; init; }
        public IncludeCommentVM Comment { get; init; }
    }
}
