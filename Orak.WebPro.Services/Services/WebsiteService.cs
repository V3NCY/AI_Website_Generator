using Microsoft.EntityFrameworkCore;
using Orak.WebPro.Data.Context;
using Orak.WebPro.Data.Entities;
using Orak.WebPro.Data.Enums;
using Orak.WebPro.Services.Interfaces;
using Orak.WebPro.Shared.DTOs.Websites;

namespace Orak.WebPro.Services.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly OrakWebProDbContext _db;

        public WebsiteService(OrakWebProDbContext db)
        {
            _db = db;
        }

        public async Task<List<WebsiteListItemDto>> GetAllAsync()
        {
            return await _db.Websites
                .Select(w => new WebsiteListItemDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Slug = w.Slug,
                    IsActive = w.IsActive,
                    PrimaryDomain = w.Domains
                        .Where(d => d.IsPrimary && d.IsActive)
                        .Select(d => d.DomainName)
                        .FirstOrDefault()
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<WebsiteDetailsDto?> GetByIdAsync(int id)
        {
            return await _db.Websites
                .Where(w => w.Id == id)
                .Select(w => new WebsiteDetailsDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Slug = w.Slug,
                    IsActive = w.IsActive,
                    SiteTitle = w.Settings != null ? w.Settings.SiteTitle : null,
                    Domains = w.Domains
                        .Where(d => d.IsActive)
                        .Select(d => d.DomainName)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(CreateWebsiteRequestDto request)
        {
            var normalizedName = request.Name.Trim();
            var normalizedSlug = request.Slug.Trim().ToLowerInvariant();
            var normalizedPrimaryDomain = string.IsNullOrWhiteSpace(request.PrimaryDomain)
                ? null
                : request.PrimaryDomain.Trim().ToLowerInvariant();

            var slugExists = await _db.Websites.AnyAsync(x => x.Slug == normalizedSlug);
            if (slugExists)
                throw new InvalidOperationException("A website with this slug already exists.");

            if (!string.IsNullOrWhiteSpace(normalizedPrimaryDomain))
            {
                var domainExists = await _db.WebsiteDomains.AnyAsync(x => x.DomainName == normalizedPrimaryDomain);
                if (domainExists)
                    throw new InvalidOperationException("This domain is already assigned to another website.");
            }

            var website = new Website
            {
                Name = normalizedName,
                Slug = normalizedSlug,
                IsActive = request.IsActive
            };

            _db.Websites.Add(website);
            await _db.SaveChangesAsync();

            var settings = new WebsiteSetting
            {
                WebsiteId = website.Id,
                SiteTitle = normalizedName,
                MetaTitle = normalizedName,
                MetaDescription = $"{normalizedName} official website"
            };

            _db.WebsiteSettings.Add(settings);

            if (!string.IsNullOrWhiteSpace(normalizedPrimaryDomain))
            {
                _db.WebsiteDomains.Add(new WebsiteDomain
                {
                    WebsiteId = website.Id,
                    DomainName = normalizedPrimaryDomain,
                    IsPrimary = true,
                    IsActive = true,
                    DomainType = DomainType.Custom
                });
            }

            await _db.SaveChangesAsync();
            return website.Id;
        }
    }
}