namespace Orak.WebPro.Admin.Models
{
    public class WebsiteInstance
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Domain { get; set; } = "";
        public string CmsStatus { get; set; } = "";
        public string SslStatus { get; set; } = "";
        public string MonitoringStatus { get; set; } = "";
        public DateTime LastUpdated { get; set; }
    }
}