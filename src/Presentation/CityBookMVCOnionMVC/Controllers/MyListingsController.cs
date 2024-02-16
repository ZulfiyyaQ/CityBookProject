﻿using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MyListingsController : Controller
    {
        private readonly IPlaceService _service;

        public MyListingsController(IPlaceService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View();
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
            if (result)
            {
                return View(update);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
