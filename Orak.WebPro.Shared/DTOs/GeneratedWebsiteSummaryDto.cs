using Orak.WebPro.Shared.Enums;

namespace Orak.WebPro.Shared.DTOs
{
    /// <summary>
    /// Lightweight summary of a generated website — used for listing in the client portal and admin panel.
    /// </summary>
    public class GeneratedWebsiteSummaryDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Display name of the institution (e.g. "СУ Христо Ботев").
        /// </summary>
        public string InstitutionName { get; set; } = string.Empty;

        /// <summary>
        /// The type of institution (University, School, Kindergarten, etc.)
        /// </summary>
        public InstitutionType InstitutionType { get; set; }

        /// <summary>
        /// The subdomain slug used for the generated site (e.g. "hristo-botev").
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Full URL of the live generated site.
        /// </summary>
        public string LiveUrl { get; set; } = string.Empty;

        /// <summary>
        /// When the site was first generated.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When the site was last republished.
        /// </summary>
        public DateTime? LastPublishedAt { get; set; }

        /// <summary>
        /// ID of the client who owns this site.
        /// </summary>
        public int ClientId { get; set; }
    }
}