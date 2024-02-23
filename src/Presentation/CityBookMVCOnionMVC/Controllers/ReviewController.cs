using CityBookMVCOnionApplication.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IPlaceService _service;
        private readonly IUserService _Userservice;

        public ReviewController(IPlaceService service, IUserService userservice)
        {
            _service = service;
            _Userservice = userservice;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _Userservice.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
