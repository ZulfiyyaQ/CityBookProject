namespace CityBookMVCOnionDomain.Entities
{
    public class EmployeeReview
    {
        public string? Text { get; set; }
        public float RatingStar { get; set; }
        //Relational props
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public User User { get; set; }
    }
}
