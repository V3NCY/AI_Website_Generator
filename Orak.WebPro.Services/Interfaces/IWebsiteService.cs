using Orak.WebPro.Shared.DTOs.Websites;

namespace Orak.WebPro.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task<List<WebsiteListItemDto>> GetAllAsync();
        Task<WebsiteDetailsDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateWebsiteRequestDto request);
    }
}