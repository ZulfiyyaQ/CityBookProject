using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class PlaceController : Controller
    {
        private readonly IPlaceService _service;

        public PlaceController(IPlaceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int categoryId, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search,categoryId, 10, page, order));
        }
        public async Task<IActionResult> DeletedItems(string? search, int categoryId, int order = 1, int page = 1)
        {
            return View(model: await _service.GetDeleteFilteredAsync(search, 10,categoryId, page, order));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReverseSoftDelete(int id)
        {
            await _service.ReverseSoftDeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        public async Task<IActionResult> Detail(int id)
        {
            GetPlaceVM get = await _service.GetByIdAsync(id);

            return View(get);
        }
    }
}
