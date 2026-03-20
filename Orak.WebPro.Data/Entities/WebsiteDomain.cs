using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Orak.WebPro.Data.Enums;

namespace Orak.WebPro.Data.Entities
{
    public class WebsiteDomain
    {
        public int Id { get; set; }

        [Required]
        public int WebsiteId { get; set; }

        [Required]
        [MaxLength(255)]
        public string DomainName { get; set; } = null!;

        public bool IsPrimary { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public DomainType DomainType { get; set; } = DomainType.Custom;

        [ForeignKey(nameof(WebsiteId))]
        public virtual Website Website { get; set; } = null!;
    }
}