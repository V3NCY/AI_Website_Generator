using System.IO;
using System.Text.Json;

namespace Orak.WebPro.Admin.Services
{
    public class JsonDataService
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public List<T> LoadList<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }

        public void SaveList<T>(string filePath, List<T> items)
        {
            var json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(filePath, json);
        }
    }
}