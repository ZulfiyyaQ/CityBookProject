using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Areas.Admin.Controllers
{
    public class HomeReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
