using Orak.WebPro.Shared.DTOs;

namespace Orak.WebPro.Services.Services
{
    /// <summary>
    /// Defines the contract for generating client websites from templates.
    /// </summary>
    public interface IWebsiteGeneratorService
    {
        /// <summary>
        /// Generates a full website from a template + client data.
        /// Returns a GenerateResult with success status, slug, and output folder.
        /// </summary>
        Task<GenerateResult> GenerateAsync(GenerateWebsiteDto dto);
    }
}