using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class TechnicalSupportWindow : Window
    {
        private List<TechIssue> issues; 

        public TechnicalSupportWindow()
        {
            InitializeComponent();
            LoadIssues();
        }
        public async void MonitorIssuesWithAI()
        {
            foreach (var issue in issues)
            {
                if (issue.Status != "Приключен") 
                {
                    (issue.AiSuggestedStatus, issue.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(issue.Issue);
                }
            }
            IssuesList.Items.Refresh();
        }

        private void LoadIssues()
        {
            issues = new List<TechIssue>
            {
                new TechIssue { Issue = "Проблем: Проблем във мобилната версия на уебсайта", AssignedTo = "Дизайнер", Status = "В процес", LastUpdatedBy = "Иван Петров" },
                new TechIssue { Issue = "Проблем: Липсва SSL Certificate", AssignedTo = "Технически екип", Status = "Очаква", LastUpdatedBy = "Мария Георгиева" },
                new TechIssue { Issue = "Проблем: Проблем със зареждане на сайта", AssignedTo = "Технически екип", Status = "Приключен", LastUpdatedBy = "Борислав Христов" }
            };

            IssuesList.ItemsSource = issues;
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue selectedIssue)
            {
                if (selectedIssue.Status == "Приключен")
                {
                    MessageBox.Show("Тази заявка е приключена. Екипът може да регистрира домейна.",
                                    "Статус приключен", MessageBoxButton.OK, MessageBoxImage.Information);

                    AddDomainWindow addDomainWindow = new AddDomainWindow();
                    if (addDomainWindow.ShowDialog() == true)
                    {
                        Domain.AddDomain(addDomainWindow.NewDomain);

                    }
                }
                else
                {
                    MessageBox.Show("Само приключени заявки могат да добавят домейн!",
                                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private async void AddIssue_Click(object sender, RoutedEventArgs e)
        {
            string newIssueDescription = "A new website is not loading properly.";
            TechIssue newIssue = new TechIssue
            {
                Issue = newIssueDescription,
                AssignedTo = "Technical Team",
                Status = "Очаква",
                LastUpdatedBy = "System"
            };

            (newIssue.AiSuggestedStatus, newIssue.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(newIssueDescription);

            issues.Add(newIssue);
            IssuesList.Items.Refresh();
        }
        
        private async void AutoUpdateIssues_Click(object sender, RoutedEventArgs e)
        {
            foreach (var issue in issues)
            {
                if (issue.Status != "Приключен") 
                {
                    (issue.AiSuggestedStatus, issue.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(issue.Issue);
                }
            }
            IssuesList.Items.Refresh();
            MessageBox.Show("AI has updated issue statuses and recommended actions.", "AI Monitoring", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}

    public class TechIssue
    {
        public string Issue { get; set; } = "Неизвестен проблем";
        public string AssignedTo { get; set; } = "Неопределен";
        public string Status { get; set; } = "Очаква";
        public string LastUpdatedBy { get; set; } = "Системата";
        public string AiSuggestedStatus { get; set; } = "Анализира се...";
        public string AiRecommendedAction { get; set; } = "Очаква анализ...";
    }




