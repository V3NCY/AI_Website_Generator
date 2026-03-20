using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Orak.WebPro.Data.Entities
{
    public class Website
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Slug { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOnUtc { get; set; }

        public virtual WebsiteSetting? Settings { get; set; }

        public virtual ICollection<WebsiteDomain> Domains { get; set; } = new List<WebsiteDomain>();
    }
}