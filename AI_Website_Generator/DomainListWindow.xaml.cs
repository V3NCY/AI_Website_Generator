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
        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string query = txtSearch.Text?.Trim().ToLowerInvariant();

            if (DomainsList.ItemsSource == null)
                return;

            var allDomains = Domain.GetDomains();

            if (string.IsNullOrWhiteSpace(query))
            {
                DomainsList.ItemsSource = allDomains;
                return;
            }

            DomainsList.ItemsSource = allDomains.FindAll(d =>
                (d.Owner ?? "").ToLower().Contains(query) ||
                (d.City ?? "").ToLower().Contains(query) ||
                (d.NewDomainName ?? "").ToLower().Contains(query) ||
                (d.OwnerEmail ?? "").ToLower().Contains(query)
            );
        }
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadDomains();
        }


    }
}
