namespace CityBookMVCOnionDomain.Entities
{
    public class Reservation : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public User? User { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int? Persons { get; set; }
        public bool IsApproved { get; set; }
        public string ReservationDate { get; set; }
        public string? ReservationDateTo { get; set; }
        public string About { get; set; } = null!;
    }
}
