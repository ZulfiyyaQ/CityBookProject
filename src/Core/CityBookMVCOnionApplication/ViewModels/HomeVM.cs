namespace CityBookMVCOnionApplication.ViewModels
{
    public record HomeVM
    {
        public PaginationVM<PlaceFilterVM> Pagination { get; set; }
        public ICollection<ItemServiceVM> Services { get; set; }
        public ICollection<ItemBlogVM> Blogs { get; set; }
        public ICollection<ItemPlaceVM> Places { get; set; }
        public ICollection<ItemCategoryVM> Categories { get; set; }
        public ICollection<ItemHomeReviewVM> HomeReviews { get; set; }
    }
   
}
