using Microsoft.AspNetCore.Identity;

namespace CityBookMVCOnionDomain.Entities
{
    public class User :IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public bool IsActivate { get; set; }
        //Relational props
        public List<Review>? Reviews { get; set; }
        public List<EmployeeReview> EmpReviews { get; set; }
        public List<Reply>? Replies { get; set; }
    }
}
