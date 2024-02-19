using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AddBlogController : Controller
    {
        private readonly IBlogService _service;

        public AddBlogController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Create()
        {
            CreateBlogVM create = new CreateBlogVM();
            await _service.CreatePopulateDropdowns(create);
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM create)
        {
            bool result = await _service.CreateAsync(create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
