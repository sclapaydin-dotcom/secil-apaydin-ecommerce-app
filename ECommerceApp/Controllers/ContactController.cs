using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(AppDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(ContactMessage model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            model.CreatedAt = DateTime.Now;
            model.IsRead = false;
            _db.ContactMessages.Add(model);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Mesajınız başarıyla iletildi. En kısa sürede dönüş yapacağız.";
            return RedirectToAction("Index");
        }
    }
}
