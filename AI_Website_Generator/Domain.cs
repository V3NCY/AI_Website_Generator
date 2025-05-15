using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AI_Website_Generator
{
    public class Domain
    {
        public string NewDomainName { get; set; }
        public string OldDomainName { get; set; }
        public string Owner { get; set; }
        public string OwnerEmail { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public string Mol { get; set; }
        public string Phone { get; set; }
        public string Package { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public string Hosting { get; set; }
        public string RequestTeamMember { get; set; }
        public string RegistrationDate { get; set; }
        public string TestDomainDate { get; set; }
        public string OfficialDomainDate { get; set; }
        public string Status { get; set; } = "Нов";

        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "domains.json");

        public static List<Domain> GetDomains()
        {
            if (!File.Exists(FilePath))
                return new List<Domain>();

            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Domain>>(json) ?? new List<Domain>();
        }

        public static void SaveDomains(List<Domain> domains)
        {
            var json = JsonConvert.SerializeObject(domains, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public static void AddDomain(Domain domain)
        {
            var all = GetDomains();
            all.Add(domain);
            SaveDomains(all);
        }
    }
}
