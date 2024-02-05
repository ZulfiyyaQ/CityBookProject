namespace CityBookMVCOnionApplication.ViewModels.Review
{
    public record CreateReviewVM
    {
        public string? Text { get; init; }
        public float RatingStar { get; init; }
       
        public int UserId { get; init; }

       
    }
}
