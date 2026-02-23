using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AppDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Sliders = await _db.Sliders
                .Where(s => s.IsActive)
                .OrderBy(s => s.OrderNo)
                .ToListAsync();

            ViewBag.FeaturedProducts = await _db.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.IsFeatured)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .ToListAsync();

            ViewBag.Categories = await _db.Categories
                .Where(c => c.IsActive)
                .ToListAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
