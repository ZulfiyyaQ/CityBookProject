using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityBookMVCOnionMVC.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IUserService _service;

        public MyAccountController(IUserService service)
        {
            _service = service;
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
            return RedirectToAction("Index","Home");
        }
    }
}
