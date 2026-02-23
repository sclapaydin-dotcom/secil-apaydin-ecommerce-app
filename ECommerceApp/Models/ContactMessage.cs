using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [MaxLength(100)]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        [MaxLength(150)]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        [Display(Name = "Konu")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [Display(Name = "Mesaj")]
        public string Message { get; set; } = string.Empty;

        [Display(Name = "Gönderim Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Okundu")]
        public bool IsRead { get; set; } = false;
    }
}
