﻿

namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeCommentVM
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public DateTime CreateAt { get; init; }
        public List<IncludeReplyVM>? Replies { get; init; }
        public IncludeUserVM? User { get; init; }
    }
}
