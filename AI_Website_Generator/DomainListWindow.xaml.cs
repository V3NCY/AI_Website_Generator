using System.Collections.Generic;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class DomainListWindow : Window
    {
        private List<Domain> domains = Domain.GetDomains();

        public DomainListWindow()
        {
            InitializeComponent();
            LoadDomains();  
        }

        private void LoadDomains()
        {
            DomainsList.Items.Clear();  
            foreach (var domain in domains)
            {
                DomainsList.Items.Add($"🌍 {domain.DomainName} - 👤 {domain.Owner} - 🔒 {domain.Status} - 📅 {domain.RegistrationDate}");
            }
        }

        private void RefreshDomains_Click(object sender, RoutedEventArgs e)
        {
            LoadDomains(); 
        }
    }
}
