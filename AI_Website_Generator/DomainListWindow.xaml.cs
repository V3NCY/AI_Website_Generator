using System.Collections.Generic;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class DomainListWindow : Window
    {
        public DomainListWindow()
        {
            InitializeComponent();
            LoadDomains();
        }

        private void LoadDomains()
        {
            var domains = Domain.GetDomains();
            DomainsList.ItemsSource = null;
            DomainsList.ItemsSource = domains;
        }

        private void RefreshDomains_Click(object sender, RoutedEventArgs e)
        {
            LoadDomains();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenAddDomain_Click(object sender, RoutedEventArgs e)
        {
            // Opens your AddDomainWindow (if it exists)
            var w = new AddDomainWindow();
            w.Owner = this;
            w.ShowDialog();

            // Refresh after adding
            LoadDomains();
        }
    }
}
