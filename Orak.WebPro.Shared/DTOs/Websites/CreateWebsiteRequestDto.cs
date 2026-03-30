namespace Orak.WebPro.Shared.DTOs.Websites
{
    public class CreateWebsiteRequestDto
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? PrimaryDomain { get; set; }
        public bool IsActive { get; set; } = true;
    }
}