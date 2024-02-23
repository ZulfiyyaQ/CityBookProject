using CityBookMVCOnionApplication.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IPlaceService _service;
        private readonly IUserService _Userservice;

        public BookingController(IPlaceService service, IUserService userservice)
        {
            _service = service;
            _Userservice = userservice;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _Userservice.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public async Task<IActionResult> AcceptReserv(int id)
        {
            await _service.AcceptReservation(id);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> RejectReserv(int id)
        {
            await _service.CanceledReservation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
