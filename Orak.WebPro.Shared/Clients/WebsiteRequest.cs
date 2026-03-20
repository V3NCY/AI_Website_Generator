namespace Orak.WebPro.Shared
{
    public class WebsiteRequest
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; } = "";
        public string WebsiteType { get; set; } = "";
        public string Status { get; set; } = "";
        public string AssignedTo { get; set; } = "";
        public DateTime CreatedOn { get; set; }
        public string Notes { get; set; } = "";
    }
}