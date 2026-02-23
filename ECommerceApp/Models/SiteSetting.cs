using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Site adı zorunludur.")]
        [MaxLength(100)]
        [Display(Name = "Site Adı")]
        public string SiteName { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Site Açıklaması")]
        public string? SiteDescription { get; set; }

        [MaxLength(30)]
        [Display(Name = "Telefon")]
        public string? PhoneNumber { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string? Email { get; set; }

        [MaxLength(300)]
        [Display(Name = "Adres")]
        public string? Address { get; set; }

        [Display(Name = "Facebook")]
        public string? FacebookUrl { get; set; }

        [Display(Name = "Instagram")]
        public string? InstagramUrl { get; set; }

        [Display(Name = "Twitter")]
        public string? TwitterUrl { get; set; }
    }
}
