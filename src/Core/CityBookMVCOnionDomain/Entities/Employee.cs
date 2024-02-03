namespace CityBookMVCOnionDomain.Entities
{
    public class Employee : BaseNameableEntity
    {
        public string Image { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string Face { get; set; }
        public string Tvit { get; set; }
        public string Link { get; set; }
        //Relational props
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int EmpReviewId { get; set; }
        public  EmployeeReview EmpReview { get; set; }
    }
}
