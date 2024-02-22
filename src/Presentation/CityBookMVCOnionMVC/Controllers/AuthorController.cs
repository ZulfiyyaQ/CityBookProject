using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUserService _service;

        public AuthorController(IUserService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(model: await _service.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
