using ECommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.Migrate();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByEmailAsync("admin@ecommerce.com") == null)
            {
                var admin = new AppUser
                {
                    UserName = "admin@ecommerce.com",
                    Email = "admin@ecommerce.com",
                    FullName = "Site Yöneticisi",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (!context.SiteSettings.Any())
            {
                context.SiteSettings.Add(new SiteSetting
                {
                    SiteName = "Moda Store",
                    SiteDescription = "Kaliteli ürünler, uygun fiyatlar",
                    PhoneNumber = "+90 212 000 00 00",
                    Email = "info@modasstore.com",
                    Address = "İstanbul, Türkiye",
                    FacebookUrl = "#",
                    InstagramUrl = "#",
                    TwitterUrl = "#"
                });
                await context.SaveChangesAsync();
            }

            if (!context.Abouts.Any())
            {
                context.Abouts.Add(new About
                {
                    Title = "Biz Kimiz?",
                    Content = "Moda Store olarak 2010 yılından bu yana müşterilerimize en kaliteli ürünleri sunuyoruz. Müşteri memnuniyeti bizim için her şeyden önce gelir. Geniş ürün yelpazemiz ve uygun fiyatlarımızla sizi memnun etmek için buradayız.",
                    ImageUrl = "/images/about-01.jpg",
                    MissionTitle = "Misyonumuz",
                    MissionContent = "Müşterilerimize en iyi alışveriş deneyimini sunmak, kaliteli ürünleri uygun fiyatlarla ulaştırmak ve her zaman güvenilir bir marka olmak temel misyonumuzdur.",
                    MissionImageUrl = "/images/about-02.jpg"
                });
                await context.SaveChangesAsync();
            }

            if (!context.Sliders.Any())
            {
                context.Sliders.AddRange(
                    new Slider
                    {
                        Title = "Kadın Koleksiyonu",
                        SubTitle = "2024 Yeni Sezon",
                        ButtonText = "Alışverişe Başla",
                        ButtonUrl = "/Urun",
                        ImageUrl = "/images/slide-01.jpg",
                        IsActive = true,
                        OrderNo = 1
                    },
                    new Slider
                    {
                        Title = "Erkek Koleksiyonu",
                        SubTitle = "Şık ve Rahat",
                        ButtonText = "Keşfet",
                        ButtonUrl = "/Urun",
                        ImageUrl = "/images/slide-02.jpg",
                        IsActive = true,
                        OrderNo = 2
                    },
                    new Slider
                    {
                        Title = "Aksesuar Dünyası",
                        SubTitle = "Trendleri Yakala",
                        ButtonText = "İncele",
                        ButtonUrl = "/Urun",
                        ImageUrl = "/images/slide-03.jpg",
                        IsActive = true,
                        OrderNo = 3
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Kadın", IsActive = true },
                    new Category { Name = "Erkek", IsActive = true },
                    new Category { Name = "Ayakkabı", IsActive = true },
                    new Category { Name = "Çanta", IsActive = true },
                    new Category { Name = "Saat", IsActive = true },
                    new Category { Name = "Aksesuar", IsActive = true }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var categories = context.Categories.ToList();
                var kadinId = categories.First(c => c.Name == "Kadın").Id;
                var erkeId = categories.First(c => c.Name == "Erkek").Id;
                var ayakkabiId = categories.First(c => c.Name == "Ayakkabı").Id;
                var cantaId = categories.First(c => c.Name == "Çanta").Id;
                var saatId = categories.First(c => c.Name == "Saat").Id;

                context.Products.AddRange(
                    new Product { Name = "Çiçekli Elbise", Description = "Yazlık çiçekli elbise, hafif kumaş", Price = 299.90m, ImageUrl = "/images/product-01.jpg", CategoryId = kadinId, IsActive = true, IsFeatured = true },
                    new Product { Name = "Beyaz Gömlek", Description = "Slim fit erkek gömlek", Price = 189.90m, ImageUrl = "/images/product-02.jpg", CategoryId = erkeId, IsActive = true, IsFeatured = true },
                    new Product { Name = "Deri Çanta", Description = "Hakiki deri kadın çantası", Price = 499.90m, ImageUrl = "/images/product-03.jpg", CategoryId = cantaId, IsActive = true, IsFeatured = true },
                    new Product { Name = "Spor Ayakkabı", Description = "Rahat günlük spor ayakkabı", Price = 349.90m, ImageUrl = "/images/product-04.jpg", CategoryId = ayakkabiId, IsActive = true, IsFeatured = true },
                    new Product { Name = "Keten Pantolon", Description = "Yazlık keten kumaş pantolon", Price = 219.90m, ImageUrl = "/images/product-05.jpg", CategoryId = erkeId, IsActive = true, IsFeatured = false },
                    new Product { Name = "Elbise Saat", Description = "Klasik tasarım elbise saati", Price = 799.90m, ImageUrl = "/images/product-06.jpg", CategoryId = saatId, IsActive = true, IsFeatured = true },
                    new Product { Name = "Yazlık Bluz", Description = "Renkli yazlık bluz", Price = 159.90m, ImageUrl = "/images/product-07.jpg", CategoryId = kadinId, IsActive = true, IsFeatured = false },
                    new Product { Name = "Klasik Ceket", Description = "Slim fit erkek ceket", Price = 599.90m, ImageUrl = "/images/product-08.jpg", CategoryId = erkeId, IsActive = true, IsFeatured = false }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
