using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Areas.Admin.Controllers
{
    public class FeatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
