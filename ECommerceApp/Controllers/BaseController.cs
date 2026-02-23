using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AppDbContext _db;

        public BaseController(AppDbContext db)
        {
            _db = db;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ViewBag.FooterCategories = await _db.Categories.Where(c => c.IsActive).ToListAsync();
            ViewBag.SiteSetting = await _db.SiteSettings.FirstOrDefaultAsync();
            await next();
        }
    }
}
