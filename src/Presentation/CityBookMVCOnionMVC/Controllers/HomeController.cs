using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IBlogService _blogService;
        private readonly IPlaceService _placeService;
        private readonly ICategoryService _categoryService;
        private readonly IHomeReviewService _homereviewService;

        public HomeController(IServiceService serviceService, 
            IBlogService blogService, 
            IPlaceService placeService,
            ICategoryService categoryService, 
            IHomeReviewService homereviewService)
        {
            _serviceService = serviceService;
            _blogService = blogService;
            _placeService = placeService;
            _categoryService = categoryService;
            _homereviewService = homereviewService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM home = new HomeVM
            {
                Pagination = await _placeService.GetAllWhereByOrderFilterAsync(3,1),
                Services = await _serviceService.GetAllWhereAsync(3, 1),
                Places = await _placeService.GetAllWhereAsync(8, 1),
                Categories = await _categoryService.GetAllWhereAsync(8, 1),
                HomeReviews = await _homereviewService.GetAllWhereAsync(8, 1),
                Blogs = await _blogService.GetAllWhereAsync(3, 1),
                
            };
            return View(home);
        }
    }
}
