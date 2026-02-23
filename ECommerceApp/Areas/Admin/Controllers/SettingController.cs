using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var setting = await _context.SiteSettings.FirstOrDefaultAsync();
            if (setting == null)
                setting = new SiteSetting { SiteName = "" };
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SiteSetting model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = await _context.SiteSettings.FirstOrDefaultAsync();
            if (existing == null)
            {
                _context.SiteSettings.Add(model);
            }
            else
            {
                existing.SiteName = model.SiteName;
                existing.SiteDescription = model.SiteDescription;
                existing.PhoneNumber = model.PhoneNumber;
                existing.Email = model.Email;
                existing.Address = model.Address;
                existing.FacebookUrl = model.FacebookUrl;
                existing.InstagramUrl = model.InstagramUrl;
                existing.TwitterUrl = model.TwitterUrl;
                _context.SiteSettings.Update(existing);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Site ayarları güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
