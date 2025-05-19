using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class TeamManagementWindow : Window
    {
        private List<TeamMember> allTeamMembers = new(); // original full list
        private List<TeamMember> filteredMembers = new(); // for search/display

        public TeamManagementWindow()
        {
            InitializeComponent();
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            allTeamMembers = new List<TeamMember>
            {
                new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" },
                new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" },
                new TeamMember { Name = "Denka Arabadzhiyska", Role = "Request Picker" },
                new TeamMember { Name = "Borislava Dimova", Role = "Request Picker" },
                new TeamMember { Name = "Victoria Dobreva", Role = "Request Picker" },
                new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" },
                new TeamMember { Name = "Georgi Benev", Role = "Tech Team" },
                new TeamMember { Name = "Katya Kalcheva", Role = "Tester" },
                new TeamMember { Name = "Kremena Kairyakova", Role = "Tester" },
                new TeamMember { Name = "Yordan Totev", Role = "Seller"},
                new TeamMember { Name = "Hristina Boeva", Role = "Seller"},
                new TeamMember { Name = "Hristina Ilcheva", Role = "Seller"},
                new TeamMember { Name = "Tsvetan Karabov", Role = "Seller"},
                new TeamMember { Name = "Nia Yordanova", Role = "Seller"},
            };

            filteredMembers = new List<TeamMember>(allTeamMembers);
            TeamList.ItemsSource = filteredMembers;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(query))
            {
                filteredMembers = new List<TeamMember>(allTeamMembers);
            }
            else
            {
                filteredMembers = allTeamMembers
                    .Where(tm => tm.Name.ToLower().Contains(query) || tm.Role.ToLower().Contains(query))
                    .ToList();
            }

            TeamList.ItemsSource = filteredMembers;
        }

        private void AddTeamMember_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Въведете името:", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(name)) return;

            string role = Microsoft.VisualBasic.Interaction.InputBox("Въведете роля (напр. Designer, Seller):", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(role)) return;

            var newMember = new TeamMember { Name = name.Trim(), Role = role.Trim() };
            allTeamMembers.Add(newMember);
            Search_Click(null, null);
        }

        private void EditTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is TeamMember selected)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Редактирай името:", "Редакция", selected.Name);

                string newRole = Microsoft.VisualBasic.Interaction.InputBox(
                    "Редактирай ролята:", "Редакция", selected.Role);

                if (!string.IsNullOrWhiteSpace(newName))
                    selected.Name = newName.Trim();

                if (!string.IsNullOrWhiteSpace(newRole))
                    selected.Role = newRole.Trim();

                TeamList.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Моля, изберете член от екипа за редакция.", "Няма избран елемент", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void RemoveTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is not TeamMember selected) return;

            var result = MessageBox.Show($"Сигурни ли сте, че искате да премахнете {selected.Name}?",
                "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                allTeamMembers.Remove(selected);
                filteredMembers.Remove(selected);
                TeamList.ItemsSource = null;
                TeamList.ItemsSource = filteredMembers;
            }
        }
    }

    public class TeamMember
    {
        public string Name { get; set; } = "Неизвестно лице";
        public string Role { get; set; } = "Без роля";
    }
}
