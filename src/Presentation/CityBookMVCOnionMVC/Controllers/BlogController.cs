﻿using CityBookMVCOnionApplication.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int order = 1, int page = 1)
        {
            return View(await _service.GetFilteredAsync(search, 6, page, order));
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _service.GetByIdAsync(id));
        }

        public async Task<IActionResult> Comment(int blogId, string comment)
        {
            await _service.CommentAsync(blogId, comment, ModelState);

            return RedirectToAction("Detail", "Blog", new { Id = blogId });
        }
        public async Task<IActionResult> Reply(int blogId, int blogCommnetId, string comment)
        {
            await _service.ReplyAsync(blogCommnetId, comment, ModelState);

            return RedirectToAction("Detail", "Blog", new { Id = blogId });
        }
    }
}
