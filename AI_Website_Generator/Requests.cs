namespace AI_Website_Generator
{
    public class Request
    {
        public string Client { get; set; } = "Неизвестен клиент";
        public string Email { get; set; } = "example@example.com";
        public string Phone { get; set; } = "0000000000";
        public string Code { get; set; } = "N/A";
        public string Institute { get; set; } = "Неизвестна институция";
        public string City { get; set; } = "Неизвестен град";
        public string NewDomain { get; set; } = "N/A";
        public string PrevDomain { get; set; } = "N/A";
        public string Status { get; set; } = "Очаква";
        public string Template { get; set; } = "Избран темплейт";
        public string Comment { get; set; } = "";

    }
}
