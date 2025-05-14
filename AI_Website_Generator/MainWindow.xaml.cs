using System.Windows;
using System.IO;

namespace AI_Website_Generator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenAddDomainWindow_Click(object sender, RoutedEventArgs e)
        {
            AddDomainWindow addDomainWindow = new AddDomainWindow();
            addDomainWindow.ShowDialog();
        }

        private void AddNewDomain_Click(object sender, RoutedEventArgs e)
        {
            AddDomainWindow addDomainWindow = new AddDomainWindow();
            addDomainWindow.Show();
        }

        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            ViewRequestsWindow requestsWindow = new ViewRequestsWindow();
            requestsWindow.Show();
        }

        private void btnManageDesigns_Click(object sender, RoutedEventArgs e)
        {
            ManageDesignsWindow designsWindow = new ManageDesignsWindow();
            designsWindow.Show();
        }

        private void btnTechSupport_Click(object sender, RoutedEventArgs e)
        {
            TechnicalSupportWindow supportWindow = new TechnicalSupportWindow();
            supportWindow.Show();
        }
        private void btnManageTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamManagementWindow teamWindow = new TeamManagementWindow();
            teamWindow.ShowDialog();
        }
        private async void AutoUpdateIssues_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AI is now monitoring issues and updating statuses.", "AI Monitoring", MessageBoxButton.OK, MessageBoxImage.Information);

            TechnicalSupportWindow supportWindow = new TechnicalSupportWindow();
            await Task.Run(() => supportWindow.MonitorIssuesWithAI());

            MessageBox.Show("AI has updated issue statuses and recommended actions.", "AI Monitoring Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OpenWebsiteStatistics_Click(object sender, RoutedEventArgs e)
        {
            WebsiteStatisticsWindow statsWindow = new WebsiteStatisticsWindow();
            statsWindow.Show();
        }
        private void OpenDomainList_Click(object sender, RoutedEventArgs e)
        {
            DomainListWindow domainWindow = new DomainListWindow();
            domainWindow.Show();
        }
        private void OpenChatbotWindow_Click(object sender, RoutedEventArgs e)
        {
            // Open Chatbot on a server, instead of file://
            string uri = "http://localhost:8000/templates/ChatBotLink.html";

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = uri,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при отваряне на браузъра: " + ex.Message);
            }
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данните са обновени успешно!", "Обновяване", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
