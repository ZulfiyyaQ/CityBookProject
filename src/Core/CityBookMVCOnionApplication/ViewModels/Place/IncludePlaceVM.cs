namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludePlaceVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ImageUrl { get; init; }
    }
}
