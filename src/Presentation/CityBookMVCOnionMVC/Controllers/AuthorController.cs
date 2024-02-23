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
        public async Task<IActionResult> Index(string id)
        {
            var res = await _service.GetByIdAsync(id);
            return View(res);
        }
    }
}
