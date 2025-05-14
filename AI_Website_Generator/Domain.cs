using System.Collections.Generic;

namespace AI_Website_Generator
{
    public class Domain
    {
        public string DomainName { get; set; }
        public string Owner { get; set; }
        public string OwnerEmail { get; set; }  
        public string Status { get; set; }
        public string RequestTeamMember { get; set; } 
        public string RegistrationDate { get; set; }

        private static List<Domain> domains = new List<Domain>
        {
            new Domain { DomainName = "example-shop.com", Owner = "John Doe", OwnerEmail = "john@example.com", Status = "Active", RequestTeamMember = "Alice", RegistrationDate = "2023-04-10" },
            new Domain { DomainName = "creative-portfolio.net", Owner = "Alice Smith", OwnerEmail = "alice@example.com", Status = "Pending", RequestTeamMember = "Bob", RegistrationDate = "2023-05-22" }
        };

        public static List<Domain> GetDomains() => domains;

        public static void AddDomain(Domain newDomain)
        {
            domains.Add(newDomain);
        }
    }
}
