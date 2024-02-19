using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MyListingsController : Controller
    {
        private readonly IPlaceService _service;
        private readonly IUserService _Userservice;

        public MyListingsController(IPlaceService service,IUserService userservice)
        {
            _service = service;
            _Userservice = userservice;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _Userservice.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public async Task<IActionResult> Update(int id)
        {
            UpdatePlaceVM update = await _service.UpdateAsync(id);
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePlaceVM update)
        {
            bool result = await _service.UpdatePostAsync(id, update, ModelState, TempData);
            if (!result)
            {
                return View(update);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            return RedirectToAction("Index", "MyListings", new { Area = "" });
        }
    }
}
