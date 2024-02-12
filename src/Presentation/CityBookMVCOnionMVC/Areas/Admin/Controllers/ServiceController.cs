using CityBookMVCOnionApplication.ViewModels.Service;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBookMVCOnionMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> service = await _context.Services.ToListAsync();
            return View(service);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM servicevm)
        {
            if (!ModelState.IsValid) return View();
            bool result = _context.Services.Any(p => p.Name.ToLower().Trim() == servicevm.Name.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bele Service artiq movcutdur");
                return View(servicevm);
            }
            Service service = new Service
            {
                Name = servicevm.Name,
                Description = servicevm.Description,
                Icon = servicevm.Icon
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Service existed = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            UpdateServiceVM vm = new UpdateServiceVM { Name = existed.Name, Description = existed.Description, Icon = existed.Icon };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateServiceVM ServiceVM)
        {
            if (!ModelState.IsValid) return View();
            Service existed = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            bool result = _context.Services.Any(c => c.Name.ToLower().Trim() == ServiceVM.Name.ToLower().Trim() && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bele Service artiq movcutdur");
                return View();
            }
            existed.Name = ServiceVM.Name;
            existed.Description = ServiceVM.Description;
            existed.Icon = ServiceVM.Icon;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Service existed = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            _context.Services.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var Service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (Service == null) return NotFound();
            return View(Service);
        }
    }
}
