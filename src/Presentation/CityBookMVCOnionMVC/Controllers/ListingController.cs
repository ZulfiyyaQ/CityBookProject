using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    public class ListingController : Controller
    {
        private readonly IPlaceService _service;

        public ListingController(IPlaceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public async Task<IActionResult> AddReservation(CreateReservationVM reservation)
        {

            return View();
        }
    }
}
