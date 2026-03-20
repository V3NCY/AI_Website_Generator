namespace Orak.WebPro.Admin.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}