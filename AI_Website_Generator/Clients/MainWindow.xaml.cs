using Orak.WebPro.Admin.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace Orak.WebPro.Admin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private LocalWebServer _localWebServer;
        private void AddNewDomain_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddDomainWindow();
            window.ShowDialog();
        }

        private void OpenDomainList_Click(object sender, RoutedEventArgs e)
        {
            var window = new DomainListWindow();
            window.ShowDialog();
        }

        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewRequestsWindow();
            window.ShowDialog();
        }

        private void btnManageDesigns_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManageDesignsWindow();
            window.ShowDialog();
        }

        private void btnTechSupport_Click(object sender, RoutedEventArgs e)
        {
            var window = new TechnicalSupportWindow();
            window.ShowDialog();
        }

        private void btnManageTeam_Click(object sender, RoutedEventArgs e)
        {
            var window = new TeamManagementWindow();
            window.ShowDialog();
        }

        private void OpenWebsiteStatistics_Click(object sender, RoutedEventArgs e)
        {
            var window = new WebsiteStatisticsWindow();
            window.ShowDialog();
        }

        private void OpenChatbotWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "http://localhost:8000/ChatBotLink.html",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при отваряне на чатбота:\n" + ex.Message);
            }
        }
    }
}