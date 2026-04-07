using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Orak.WebPro.Shared.DTOs;
using Orak.WebPro.Shared.Enums;

namespace Orak.WebPro.Services.Services
{
    public class WebsiteGeneratorService : IWebsiteGeneratorService
    {
        private readonly string _templatesPath;
        private readonly string _outputPath;

        public WebsiteGeneratorService(string templatesPath, string outputPath)
        {
            _templatesPath = templatesPath;
            _outputPath = outputPath;
        }

        public async Task<GenerateResult> GenerateAsync(GenerateWebsiteDto dto)
        {
            try
            {
                ValidateDto(dto);

                string slug = BuildSlug(dto.InstitutionName);
                string siteFolder = Path.Combine(_outputPath, slug);

                Directory.CreateDirectory(siteFolder);
                Directory.CreateDirectory(Path.Combine(siteFolder, "assets", "images"));

                string templateFile = ResolveTemplatePath(dto.TemplateType);
                string templateHtml = await File.ReadAllTextAsync(templateFile, Encoding.UTF8);

                var replacements = BuildReplacementMap(dto);

                string finalHtml = ApplyReplacements(templateHtml, replacements);
                await File.WriteAllTextAsync(
                    Path.Combine(siteFolder, "index.html"),
                    finalHtml,
                    Encoding.UTF8);

                string css = GenerateCustomCss(dto);
                await File.WriteAllTextAsync(
                    Path.Combine(siteFolder, "style.css"),
                    css,
                    Encoding.UTF8);

                await File.WriteAllTextAsync(
                    Path.Combine(siteFolder, "robots.txt"),
                    "User-agent: *\nAllow: /\n",
                    Encoding.UTF8);

                string sitemap = GenerateSitemap(dto.Domain ?? $"{slug}.bg");
                await File.WriteAllTextAsync(
                    Path.Combine(siteFolder, "sitemap.xml"),
                    sitemap,
                    Encoding.UTF8);

                return new GenerateResult
                {
                    Success = true,
                    SiteFolder = siteFolder,
                    Slug = slug,
                    Message = $"Сайтът е генериран успешно в: {siteFolder}"
                };
            }
            catch (Exception ex)
            {
                return new GenerateResult
                {
                    Success = false,
                    Message = $"Грешка при генериране: {ex.Message}"
                };
            }
        }

        private static Dictionary<string, string> BuildReplacementMap(GenerateWebsiteDto dto)
        {
            string year = dto.FoundedYear > 0
                ? $"от {dto.FoundedYear} г."
                : string.Empty;

            string motto = !string.IsNullOrWhiteSpace(dto.Motto)
                ? dto.Motto
                : DefaultMotto(dto.TemplateType);

            string primaryColor = dto.PrimaryColor ?? DefaultColor(dto.TemplateType);

            return new Dictionary<string, string>
            {
                ["{{INSTITUTION_NAME}}"] = dto.InstitutionName ?? string.Empty,
                ["{{INSTITUTION_CITY}}"] = dto.City ?? string.Empty,
                ["{{INSTITUTION_SLUG}}"] = BuildSlug(dto.InstitutionName),
                ["{{INSTITUTION_TYPE}}"] = InstitutionTypeLabel(dto.TemplateType),
                ["{{FOUNDED_YEAR}}"] = year,
                ["{{MOTTO}}"] = motto,
                ["{{STUDENTS_COUNT}}"] = dto.StudentsCount > 0 ? dto.StudentsCount.ToString() : "—",

                ["{{EMAIL}}"] = dto.Email ?? string.Empty,
                ["{{PHONE}}"] = dto.Phone ?? string.Empty,
                ["{{ADDRESS}}"] = dto.Address ?? string.Empty,
                ["{{DOMAIN}}"] = dto.Domain ?? $"{BuildSlug(dto.InstitutionName)}.bg",
                ["{{DIRECTOR_NAME}}"] = dto.DirectorName ?? string.Empty,
                ["{{NEISPUO_CODE}}"] = dto.NeispuoCode ?? string.Empty,

                ["{{PRIMARY_COLOR}}"] = primaryColor,
                ["{{PRIMARY_COLOR_DARK}}"] = Darken(primaryColor),

                ["{{GENERATED_DATE}}"] = DateTime.Now.ToString("dd.MM.yyyy"),
                ["{{GENERATOR_VERSION}}"] = "ORAK WebPro v1.0",

                ["Evergreen University"] = dto.InstitutionName ?? string.Empty,
                ["Northbridge"] = dto.InstitutionName ?? string.Empty,
                ["Rainbow Kids Kindergarten"] = dto.InstitutionName ?? string.Empty,
                ["СУ „Иван Вазов“"] = dto.InstitutionName ?? string.Empty,
                ["Университет „Св. Климент Охридски“"] = dto.InstitutionName ?? string.Empty,
                ["ДГ „Слънчице“"] = dto.InstitutionName ?? string.Empty,

                ["Knowledge • Character • Community"] = motto,
                ["Знание • Достойнство • Общност"] = motto,
                ["Играем • Учим • Растем заедно"] = motto,
                ["Play • Learn • Grow"] = motto,
                ["Основан 1888 г."] = year,
                ["от 1921 г."] = year
            };
        }

        private static string ApplyReplacements(string html, Dictionary<string, string> map)
        {
            foreach (var kv in map)
            {
                html = html.Replace(kv.Key, kv.Value ?? string.Empty, StringComparison.Ordinal);
            }

            return html;
        }

        private static string GenerateCustomCss(GenerateWebsiteDto dto)
        {
            string primary = dto.PrimaryColor ?? DefaultColor(dto.TemplateType);
            string dark = Darken(primary);

            return $@"/* Generated by ORAK WebPro on {DateTime.Now:dd.MM.yyyy} */
/* Client: {dto.InstitutionName} */

:root {{
  --primary:       {primary};
  --primary-dark:  {dark};
  --primary-light: {primary}22;
}}

.btn-primary,
.btn-navy,
.btn-sun {{ background: linear-gradient(145deg, var(--primary), var(--primary-dark)) !important; }}

.nav-item.active,
.nav-link.active {{ background: var(--primary-light) !important; border-color: var(--primary)44 !important; color: var(--primary) !important; }}

.kicker {{ background: var(--primary-light) !important; border-color: var(--primary)33 !important; color: var(--primary) !important; }}

.stat-row strong,
.stat-cell .num {{ color: var(--primary) !important; }}
";
        }

        private static string GenerateSitemap(string domain)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
  <url><loc>https://{domain}/</loc><lastmod>{today}</lastmod><priority>1.0</priority></url>
  <url><loc>https://{domain}/#/news</loc><lastmod>{today}</lastmod><priority>0.8</priority></url>
  <url><loc>https://{domain}/#/contact</loc><lastmod>{today}</lastmod><priority>0.7</priority></url>
  <url><loc>https://{domain}/#/documents</loc><lastmod>{today}</lastmod><priority>0.6</priority></url>
</urlset>";
        }

        private string ResolveTemplatePath(InstitutionType type)
        {
            string file = type switch
            {
                InstitutionType.School => "templateB.html",
                InstitutionType.University => "templateA.html",
                InstitutionType.Kindergarten => "templateC.html",
                _ => "templateB.html"
            };

            string path = Path.Combine(_templatesPath, file);

            if (!File.Exists(path))
                throw new FileNotFoundException($"Шаблонът не е намерен: {path}");

            return path;
        }

        private static string BuildSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "site";

            var sb = new StringBuilder();

            foreach (char c in name.ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
                else if (c == ' ' || c == '-')
                {
                    sb.Append('-');
                }
            }

            string slug = sb.ToString().Trim('-');

            while (slug.Contains("--"))
            {
                slug = slug.Replace("--", "-");
            }

            return string.IsNullOrWhiteSpace(slug) ? "site" : slug;
        }

        private static string DefaultColor(InstitutionType type) => type switch
        {
            InstitutionType.University => "#0C2461",
            InstitutionType.Kindergarten => "#FFB830",
            _ => "#1B4D3E"
        };

        private static string DefaultMotto(InstitutionType type) => type switch
        {
            InstitutionType.University => "Знание · Достойнство · Общност",
            InstitutionType.Kindergarten => "Играем · Учим · Растем заедно",
            _ => "Знание · Характер · Общност"
        };

        private static string InstitutionTypeLabel(InstitutionType type) => type switch
        {
            InstitutionType.University => "Университет",
            InstitutionType.Kindergarten => "Детска градина",
            _ => "Училище"
        };

        private static string Darken(string hex)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hex))
                    return "#000000";

                string cleanHex = hex.Trim().TrimStart('#');

                if (cleanHex.Length < 6)
                    return hex;

                int r = Math.Max(0, Convert.ToInt32(cleanHex.Substring(0, 2), 16) - 40);
                int g = Math.Max(0, Convert.ToInt32(cleanHex.Substring(2, 2), 16) - 40);
                int b = Math.Max(0, Convert.ToInt32(cleanHex.Substring(4, 2), 16) - 40);

                return $"#{r:X2}{g:X2}{b:X2}";
            }
            catch
            {
                return hex;
            }
        }

        private static void ValidateDto(GenerateWebsiteDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.InstitutionName))
                throw new ArgumentException("Наименованието е задължително.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Имейлът е задължителен.");

            if (string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("Населеното място е задължително.");
        }
    }

    public class GenerateResult
    {
        public bool Success { get; set; }
        public string SiteFolder { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}