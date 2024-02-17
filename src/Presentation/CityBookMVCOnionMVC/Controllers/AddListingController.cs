using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AddListingController : Controller
    {
        private readonly IPlaceService _service;

        public AddListingController(IPlaceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Create()
        {
            CreatePlaceVM create = new CreatePlaceVM();
            await _service.CreatePopulateDropdowns(create);
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePlaceVM create)
        {
            bool result = await _service.CreateAsync(create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction("Index", "Home", new { Area = "" } );
        }
       
    }
}

