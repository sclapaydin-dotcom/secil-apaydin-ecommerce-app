using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.OrderBy(s => s.OrderNo).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                using (var stream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.ImageUrl = "/uploads/" + fileName;
            }

            _context.Sliders.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Slider başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slider model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                using (var stream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.ImageUrl = "/uploads/" + fileName;
            }

            _context.Sliders.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Slider başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return NotFound();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Slider başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
