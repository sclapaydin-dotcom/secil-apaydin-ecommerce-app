using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class About
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [MaxLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "İçerik zorunludur.")]
        [Display(Name = "İçerik")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Resim")]
        public string? ImageUrl { get; set; }

        [MaxLength(200)]
        [Display(Name = "Misyon Başlığı")]
        public string? MissionTitle { get; set; }

        [Display(Name = "Misyon İçeriği")]
        public string? MissionContent { get; set; }

        [Display(Name = "Misyon Resmi")]
        public string? MissionImageUrl { get; set; }
    }
}
