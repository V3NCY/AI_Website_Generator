using Orak.WebPro.Shared.Enums;

namespace Orak.WebPro.Shared.DTOs
{
    public class GenerateWebsiteDto
    {
        public string InstitutionName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Domain { get; set; }
        public string? NeispuoCode { get; set; }
        public string? DirectorName { get; set; }
        public int FoundedYear { get; set; }
        public int StudentsCount { get; set; }
        public string? Motto { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public InstitutionType TemplateType { get; set; } = InstitutionType.School;
        public string? PrimaryColor { get; set; }
        public int WebsiteId { get; set; }
        public string? RefCode { get; set; }
    }
}