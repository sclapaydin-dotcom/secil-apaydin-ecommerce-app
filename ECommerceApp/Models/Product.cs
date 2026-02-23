using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [MaxLength(200)]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999, ErrorMessage = "Geçerli bir fiyat giriniz.")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Resim")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Kategori seçiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Öne Çıkan")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
