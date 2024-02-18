using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public async Task<IActionResult> EditUser()
        {
            return View(await _service.EditUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM editUser)
        {
            bool result = await _service.EditUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), editUser, ModelState);
            if (!result)
            {
                return View(editUser);
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}
