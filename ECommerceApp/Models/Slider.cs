using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [MaxLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Alt Başlık")]
        public string? SubTitle { get; set; }

        [MaxLength(50)]
        [Display(Name = "Buton Metni")]
        public string? ButtonText { get; set; }

        [MaxLength(200)]
        [Display(Name = "Buton Linki")]
        public string? ButtonUrl { get; set; }

        [Display(Name = "Resim")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Sıra")]
        [Range(1, 100)]
        public int OrderNo { get; set; } = 1;
    }
}
