using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record ItemReservationVM
    {
        public int Id { get; init; } 
        public string UserId { get; init; }
       
        public int PlaceId { get; init; }
        
        public int? Persons { get; init; }
        public bool IsApproved { get; init; }
        public string ReservationDate { get; init; }
        public string? ReservationDateTo { get; init; }
        public string About { get; init; } 
    }
}
