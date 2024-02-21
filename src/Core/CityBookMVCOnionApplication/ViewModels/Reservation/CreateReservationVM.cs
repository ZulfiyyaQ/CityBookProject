namespace CityBookMVCOnionApplication.ViewModels
{
    public record CreateReservationVM
    {
        public int? PlaceId { get; init; }
        public string? DayOrMonth { get; init; }
        public int? Persons { get; init; }
        public DateTime ReservationDate { get; init; }
        public DateTime? ReservationDateTo { get; init; }
        public string About { get; init; } 
    }
}
