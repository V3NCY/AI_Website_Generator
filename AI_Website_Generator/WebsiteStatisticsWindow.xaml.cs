using System.Collections.ObjectModel;
using System.Windows;
using AI_Website_Generator.Models; 

namespace AI_Website_Generator
{
    public partial class WebsiteStatisticsWindow : Window
    {
        public ObservableCollection<Website> Websites { get; set; }

        public WebsiteStatisticsWindow()
        {
            InitializeComponent();

            Websites = new ObservableCollection<Website>
            {
                new Website { Name = "Company A", Status = "Активен", ErrorDetails = "No issues detected." },
                new Website { Name = "E-Shop B", Status = "Активен", ErrorDetails = "No issues detected." },
                new Website { Name = "Blog C", Status = "Проблем", ErrorDetails = "⚠️ SSL Certificate is missing!" },
                new Website { Name = "Forum D", Status = "Проблем", ErrorDetails = "⚠️ Website loading is too slow (5+ sec)." },
                new Website { Name = "Startup E", Status = "Активен", ErrorDetails = "No issues detected." }
            };

            DataContext = this;
        }

        private void RefreshAIInsights_Click(object sender, RoutedEventArgs e)
        {
            Websites[2].ErrorDetails = "⚠️ SSL Certificate renewed!";
            Websites[3].ErrorDetails = "⚠️ Optimized loading speed.";

            MessageBox.Show("✅ AI Insights Updated!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
namespace AI_Website_Generator.Models
{
    public class Website
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string ErrorDetails { get; set; }  
    }
}
