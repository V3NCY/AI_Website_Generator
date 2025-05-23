﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Input;

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
                    Issue = "Проблем: Счупено меню в мобилна версия",
                    AssignedTo = "UX екип",
                    Status = "Очаква",
                    LastUpdatedBy = "Системата",
                    Priority = "Висок",
                    Category = "UI",
                    ClientName = "ОУ Хан Аспарух",
                    WebsiteDomain = "asprauhschool.bg",
                    BrowserInfo = "Chrome 124, Android",
                    OS = "Android 12"
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

        private void IssuesList_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (IssuesList.SelectedItem is TechIssue selectedIssue)
            {
                var result = MessageBox.Show($"Наистина ли искате да отбележите проблема като 'Приключен'?\n\n{selectedIssue.Issue}",
                                             "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    selectedIssue.Status = "Приключен";
                    selectedIssue.LastUpdatedBy = "Системата";
                    IssuesList.Items.Refresh();
                }
            }
        }

    }

    public class TechIssue
    {
        public string Issue { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string LastUpdatedBy { get; set; }

        // Нови атрибути
        public string Priority { get; set; } = "Среден"; // Нисък, Среден, Висок
        public string Category { get; set; } = "Общ";     // UI, Backend, Сигурност, Инсталация и т.н.
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ResolvedDate { get; set; } = null;
        public string ClientName { get; set; }
        public string WebsiteDomain { get; set; }
        public string BrowserInfo { get; set; }
        public string OS { get; set; }

        // AI
        public string AiSuggestedStatus { get; set; }
        public string AiRecommendedAction { get; set; }

        // За вътрешна употреба
        public bool IsUrgent => Priority == "Висок" && Status != "Приключен";
    }

}