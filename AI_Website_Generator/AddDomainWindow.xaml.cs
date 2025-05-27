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
            cmbRequestTeam.ItemsSource = TeamManagementWindow.TeamMembers;

        }

        private void AddDomain_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewDomainName.Text) ||
                string.IsNullOrWhiteSpace(txtOldDomainName.Text) ||
                string.IsNullOrWhiteSpace(txtOwner.Text) ||
                string.IsNullOrWhiteSpace(txtOwnerEmail.Text) ||
                cmbRequestTeam.SelectedItem == null)
            {
                MessageBox.Show("Моля, попълнете всички полета.", "Липсваща информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewDomain = new Domain
            {
                NewDomainName = txtNewDomainName.Text.Trim(),
                OldDomainName = txtOldDomainName.Text.Trim(),
                Owner = txtOwner.Text.Trim(),
                OwnerEmail = txtOwnerEmail.Text.Trim(),
                City = txtCity.Text.Trim(),
                Code = txtCode.Text.Trim(),
                Mol = txtMol.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Package = (cmbPackage.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                AdminUsername = txtAdminUsername.Text.Trim(),
                AdminPassword = txtAdminPassword.Text.Trim(),
                Hosting = (cmbHosting.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                RequestTeamMember = (cmbRequestTeam.SelectedItem as TeamMember)?.Name ?? "",
                RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                TestDomainDate = dateRegTestDomain.SelectedDate?.ToString("yyyy-MM-dd") ?? "",
                OfficialDomainDate = dateRegOfficialDomain.SelectedDate?.ToString("yyyy-MM-dd") ?? ""
            };

            Domain.AddDomain(NewDomain);

            this.DialogResult = true;
            Close();
        }


    }
}
