using System.Collections.Generic;

namespace Orak.WebPro.Shared.DTOs.Websites
{
    public class WebsiteDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? SiteTitle { get; set; }
        public List<string> Domains { get; set; } = new();
    }
}