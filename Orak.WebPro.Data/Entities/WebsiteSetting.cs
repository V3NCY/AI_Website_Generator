using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orak.WebPro.Data.Entities
{
    public class WebsiteSetting
    {
        public int Id { get; set; }

        [Required]
        public int WebsiteId { get; set; }

        [MaxLength(200)]
        public string? SiteTitle { get; set; }

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [MaxLength(500)]
        public string? FaviconUrl { get; set; }

        [MaxLength(255)]
        public string? MetaTitle { get; set; }

        [MaxLength(1000)]
        public string? MetaDescription { get; set; }

        [MaxLength(255)]
        public string? ContactEmail { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(1000)]
        public string? FooterText { get; set; }

        [ForeignKey(nameof(WebsiteId))]
        public virtual Website Website { get; set; } = null!;
    }
}