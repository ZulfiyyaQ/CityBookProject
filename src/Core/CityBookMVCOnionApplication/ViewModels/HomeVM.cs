namespace CityBookMVCOnionApplication.ViewModels
{
    public record HomeVM
    {
        //public PaginationVM<ProductFilterVM> Pagination { get; set; }
        public List<ItemServiceVM> Services { get; set; }
        public List<ItemBlogVM> Blogs { get; set; }
        public List<ItemPlaceVM> Places { get; set; }
        public List<ItemCategoryVM> Categories { get; set; }
        public List<ItemHomeReviewVM> HomeReviews { get; set; }
    }
   
}
