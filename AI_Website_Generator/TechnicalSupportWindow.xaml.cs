using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

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
                new TechIssue { Issue="Проблем: Мобилната версия не работи", AssignedTo="Дизайнер", Status="В процес", LastUpdatedBy="Иван" },
                new TechIssue { Issue="Проблем: Липсва SSL", AssignedTo="Технически екип", Status="Очаква", LastUpdatedBy="Мария" },
                new TechIssue { Issue="Проблем: Зареждане на сайта", AssignedTo="Технически екип", Status="Приключен", LastUpdatedBy="Борислав" }
            });

            IssuesList.ItemsSource = issues;
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue t)
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox(
                    "Въведете нов статус за проблема:",
                    "Промяна на статус",
                    t.Status);

                if (!string.IsNullOrWhiteSpace(input))
                {
                    t.Status = input.Trim();
                    IssuesList.Items.Refresh();
                }
            }
        }

        private void AddIssue_Click(object sender, RoutedEventArgs e)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Въведете описание на новия проблем:",
                "Нов проблем",
                "Проблем: ...");

            if (!string.IsNullOrWhiteSpace(input))
            {
                issues.Add(new TechIssue
                {
                    Issue = input.Trim(),
                    AssignedTo = "Неопределен",
                    Status = "Очаква",
                    LastUpdatedBy = "System"
                });

                IssuesList.Items.Refresh();
            }
        }

        private async void AnalyzeIssue_Click(object sender, RoutedEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue t)
            {
                try
                {
                    (t.AiSuggestedStatus, t.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(t.Issue);
                    MessageBox.Show($"AI анализ:\n\nСтатус: {t.AiSuggestedStatus}\nДействие: {t.AiRecommendedAction}", "AI Анализ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Възникна грешка при AI анализа:\n" + ex.Message, "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                IssuesList.Items.Refresh();
            }
        }

        private async void AutoUpdateIssues_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var t in issues)
                {
                    if (t.Status != "Приключен")
                    {
                        (t.AiSuggestedStatus, t.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(t.Issue);
                    }
                }
                IssuesList.Items.Refresh();
                MessageBox.Show("AI анализът за всички активни проблеми е завършен.", "AI Обновяване", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Грешка при масов AI анализ:\n" + ex.Message, "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void MonitorIssuesWithAI()
        {
            foreach (var t in issues)
            {
                if (t.Status != "Приключен")
                {
                    (t.AiSuggestedStatus, t.AiRecommendedAction) = await AIHelper.GetAIStatusAndAction(t.Issue);
                }
            }

            IssuesList.Items.Refresh();
        }
    }

    public class TechIssue
    {
        public string Issue { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string LastUpdatedBy { get; set; }
        public string AiSuggestedStatus { get; set; }
        public string AiRecommendedAction { get; set; }
    }
}