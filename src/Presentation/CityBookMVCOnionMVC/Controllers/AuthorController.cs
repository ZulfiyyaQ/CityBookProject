using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUserService _service;

        public AuthorController(IUserService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string? search, string? returnUrl, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 3, page, order));
        }
    }
}
