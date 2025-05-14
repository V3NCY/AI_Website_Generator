using System;
using System.Collections.Generic;

namespace AI_Website_Generator
{
    public class Website
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Status { get; set; }
        public string Traffic { get; set; }
        public string AIInsights { get; set; }
        public string ErrorDetails { get; internal set; }

        public static List<Website> GetSampleWebsites()
        {
            return new List<Website>
            {
                new Website { Name = "E-Commerce Shop", URL = "https://shop.example.com", Status = "Online", Traffic = "High", AIInsights = "Stable growth, SEO improvements needed" },
                new Website { Name = "Portfolio Site", URL = "https://portfolio.example.com", Status = "Online", Traffic = "Medium", AIInsights = "High bounce rate detected" },
                new Website { Name = "Blog", URL = "https://blog.example.com", Status = "Offline", Traffic = "N/A", AIInsights = "Site is down, server maintenance needed" }
            };
        }
    }
}
