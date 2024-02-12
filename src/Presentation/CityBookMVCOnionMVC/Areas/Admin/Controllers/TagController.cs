using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
