using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about == null)
            {
                about = new About { Title = "", Content = "" };
            }
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(About model, IFormFile? imageFile, IFormFile? missionImageFile)
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

            if (missionImageFile != null && missionImageFile.Length > 0)
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(missionImageFile.FileName);
                using (var stream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create))
                {
                    await missionImageFile.CopyToAsync(stream);
                }
                model.MissionImageUrl = "/uploads/" + fileName;
            }

            var existing = await _context.Abouts.FirstOrDefaultAsync();
            if (existing == null)
            {
                _context.Abouts.Add(model);
            }
            else
            {
                existing.Title = model.Title;
                existing.Content = model.Content;
                existing.MissionTitle = model.MissionTitle;
                existing.MissionContent = model.MissionContent;
                if (!string.IsNullOrEmpty(model.ImageUrl))
                    existing.ImageUrl = model.ImageUrl;
                if (!string.IsNullOrEmpty(model.MissionImageUrl))
                    existing.MissionImageUrl = model.MissionImageUrl;
                _context.Abouts.Update(existing);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Hakkımızda sayfası güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
