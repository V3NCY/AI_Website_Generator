namespace Orak.WebPro.Shared.DTOs.Websites
{
    public class WebsiteListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? PrimaryDomain { get; set; }
        public bool IsActive { get; set; }
    }
}