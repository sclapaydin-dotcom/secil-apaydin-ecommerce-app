using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class AboutController : BaseController
    {
        public AboutController(AppDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            var about = await _db.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
    }
}
