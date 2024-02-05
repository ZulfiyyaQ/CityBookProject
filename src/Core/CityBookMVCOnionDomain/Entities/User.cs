using Microsoft.AspNetCore.Identity;

namespace CityBookMVCOnionDomain.Entities
{
    public class User :IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string WebSite { get; set; }
        public string? Tvit { get; set; }
        public string? Inst { get; set; }
        public string? Face { get; set; }
        public string? Link { get; set; }
        public bool IsActivate { get; set; }
        //Relational props
        public List<Blog>? Blogs { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<HomeReview> EmpReviews { get; set; }
        public List<Reply>? Replies { get; set; }
    }
}
