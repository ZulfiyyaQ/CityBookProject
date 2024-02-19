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
        public async Task<IActionResult> Index(string? search, string? returnUrl, int? categoryId, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 3, page, order, categoryId));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var res = await _service.GetByIdAsync(id);
            return View(res);
         
        }
        public async Task<IActionResult> Review(int id ,int rating, string comment)
        {
            await _service.Review(id, rating, comment, ModelState);

            return RedirectToAction("Detail", "Listing", new { Id = id });
        }

        public async Task<IActionResult> AddReservation(int id,CreateReservationVM reservation)
        {
            await _service.AddReservation(id, reservation, ModelState);

            return RedirectToAction("Detail", "Listing", new { Id = id });
        }
    }
}
