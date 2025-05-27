using System.Collections.ObjectModel;
using System.Windows;
using AI_Website_Generator.Models;

namespace AI_Website_Generator
{
    public partial class WebsiteStatisticsWindow : Window
    {
        public ObservableCollection<Website> Websites { get; set; }
        public ObservableCollection<Website> FilteredWebsites { get; set; }

        public WebsiteStatisticsWindow()
        {
            InitializeComponent();

            Websites = new ObservableCollection<Website>
            {
                new Website { Name = "Template A - University", Status = "Активен", ErrorDetails = "No issues detected." },
                new Website { Name = "E-Shop B", Status = "Активен", ErrorDetails = "No issues detected." },
                new Website { Name = "Template B - Kindergarden", Status = "Проблем", ErrorDetails = "⚠️ SSL Certificate is missing!" },
                new Website { Name = "Template C - School", Status = "Проблем", ErrorDetails = "⚠️ Website loading is too slow (5+ sec)." },
                new Website { Name = "Startup E", Status = "Активен", ErrorDetails = "No issues detected." }
            };

            FilterWebsites();
            DataContext = this;
        }

        private void FilterWebsites()
        {
            FilteredWebsites = new ObservableCollection<Website>(Websites.Where(w => w.Status == "Проблем"));
        }

        private void RefreshAIInsights_Click(object sender, RoutedEventArgs e)
        {
            Websites[2].ErrorDetails = "SSL Certificate renewed.";
            Websites[2].AIInsights = "SSL check passed, performance stable.";
            Websites[2].RecommendedAction = "Monitor SSL renewal schedule.";
            Websites[2].AIConfidence = 90;

            Websites[3].ErrorDetails = "Optimized loading speed.";
            Websites[3].AIInsights = "Page speed improved by 30%.";
            Websites[3].RecommendedAction = "Compress homepage images.";
            Websites[3].AIConfidence = 85;

            MessageBox.Show("✅ AI Insights Updated!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
