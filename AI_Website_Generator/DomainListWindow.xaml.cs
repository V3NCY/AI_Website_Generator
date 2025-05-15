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
            var domains = Domain.GetDomains();
            DomainsList.ItemsSource = null;
            DomainsList.ItemsSource = domains;
        }




        private void RefreshDomains_Click(object sender, RoutedEventArgs e)
        {
            LoadDomains(); 
        }
    }
}
