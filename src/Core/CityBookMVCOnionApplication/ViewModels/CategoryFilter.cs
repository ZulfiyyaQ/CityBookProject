namespace CityBookMVCOnionApplication.ViewModels
{
    public class CategoryFilter
    {
        public PaginationVM<ItemCategoryVM> Pagination { get; set; }
        public ICollection<ItemCategoryVM> Categorys { get; set; }

    }
}
