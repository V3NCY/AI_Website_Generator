using System.Collections.Generic;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class TechnicalSupportWindow : Window
    {
        private List<TechIssue> issues = new List<TechIssue>();

        public TechnicalSupportWindow()
        {
            InitializeComponent();
            LoadStaticIssues();
        }

        private void LoadStaticIssues()
        {
            issues.AddRange(new[]
            {
                new TechIssue { Issue="Мобилната версия не работи", AssignedTo="Дизайнер", Status="В процес", Priority="Висок", Category="UI", ClientName="Училище А", WebsiteDomain="schoola.bg" },
                new TechIssue { Issue="SSL сертификат липсва", AssignedTo="СисАдмин", Status="Очаква", Priority="Среден", Category="Сигурност", ClientName="Клиент B", WebsiteDomain="clientb.bg" },
                new TechIssue { Issue="Бавен сайт", AssignedTo="СисАдмин", Status="В процес", Priority="Висок", Category="Performance", ClientName="Клиент C", WebsiteDomain="clientc.bg" }
            });

            IssuesList.ItemsSource = issues;
        }

        private void AddIssue_Click(object sender, RoutedEventArgs e)
        {
            issues.Add(new TechIssue
            {
                Issue = "Нов проблем, добавете описание...",
                AssignedTo = "Ново лице",
                Status = "Очаква",
                Priority = "Среден",
                Category = "Общ",
                ClientName = "Неизвестен",
                WebsiteDomain = "example.bg"
            });
            IssuesList.Items.Refresh();
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue t)
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox("Нов статус:", "Промяна на статус", t.Status);
                if (!string.IsNullOrWhiteSpace(input))
                {
                    t.Status = input.Trim();
                    IssuesList.Items.Refresh();
                }
            }
        }

        private void MarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue t)
            {
                t.Status = "Приключен";
                IssuesList.Items.Refresh();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            IssuesList.Items.Refresh();
        }


    }

    public class TechIssue
    {
        public string Issue { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string ClientName { get; set; }
        public string WebsiteDomain { get; set; }
        public string LastUpdatedBy { get; set; } = "Системата";
        public string CreatedDate { get; set; } = System.DateTime.Now.ToString("dd.MM.yyyy");
    }
}
