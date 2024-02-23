namespace CityBookMVCOnionApplication.ViewModels
{
    public record GetUserVM(string Id, string Name, string Surname, string UserName, string Image, string Email, 
        string? Address, string? About, string? Face, string? Tvit,  string? Link, string? Inst, string? Website
        )
    {
        public string? PhoneNumber { get; init; }
        


        public ICollection<IncludeBlogVM>? Blogs { get; init; }
        public ICollection<IncludeReviewVM>? Reviews { get; init; }
        public ICollection<IncludeReplyVM>? Replies { get; init; }
        public ICollection<IncludePlaceVM>? Places { get; init; }

    }
}
