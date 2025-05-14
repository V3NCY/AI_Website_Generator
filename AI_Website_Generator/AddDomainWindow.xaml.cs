using System;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class AddDomainWindow : Window
    {
        public Domain NewDomain { get; private set; }

        public AddDomainWindow()
        {
            InitializeComponent();
        }

        private void AddDomain_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDomainName.Text) ||
                string.IsNullOrWhiteSpace(txtOwner.Text) ||
                string.IsNullOrWhiteSpace(txtOwnerEmail.Text) ||
                string.IsNullOrWhiteSpace(txtRequestTeam.Text))
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewDomain = new Domain
            {
                DomainName = txtDomainName.Text,
                Owner = txtOwner.Text,
                OwnerEmail = txtOwnerEmail.Text,
                RequestTeamMember = txtRequestTeam.Text,
                RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd")
            };

            DialogResult = true;
            Close();
        }
    }
}
