using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class TeamManagementWindow : Window
    {
        public static ObservableCollection<TeamMember> TeamMembers { get; set; } = new ObservableCollection<TeamMember>();


        //private List<TeamMember> allTeamMembers = new(); 
        private List<TeamMember> filteredMembers = new(); 
        public TeamManagementWindow()
        {
            InitializeComponent();
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            if (TeamMembers.Count == 0)
            {
                TeamMembers.Add(new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" });
                TeamMembers.Add(new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" });
                TeamMembers.Add(new TeamMember { Name = "Denka Arabadzhiyska", Role = "Request Picker" });
                TeamMembers.Add(new TeamMember { Name = "Borislava Dimova", Role = "Request Picker" });
                TeamMembers.Add(new TeamMember { Name = "Victoria Dobreva", Role = "Request Picker" });
                TeamMembers.Add(new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" });
                TeamMembers.Add(new TeamMember { Name = "Georgi Benev", Role = "Tech Team" });
                TeamMembers.Add(new TeamMember { Name = "Katya Kalcheva", Role = "Tester" });
                TeamMembers.Add(new TeamMember { Name = "Kremena Kairyakova", Role = "Tester" });
                TeamMembers.Add(new TeamMember { Name = "Yordan Totev", Role = "Seller" });
                TeamMembers.Add(new TeamMember { Name = "Hristina Boeva", Role = "Seller" });
                TeamMembers.Add(new TeamMember { Name = "Hristina Ilcheva", Role = "Seller" });
                TeamMembers.Add(new TeamMember { Name = "Tsvetan Karabov", Role = "Seller" });
                TeamMembers.Add(new TeamMember { Name = "Nia Yordanova", Role = "Seller" });
            }

            TeamList.ItemsSource = TeamMembers;
        }



        public static ObservableCollection<TeamMember> GetTeamList()
        {
            return TeamMembers;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(query))
            {
                filteredMembers = new List<TeamMember>(TeamMembers);
            }
            else
            {
                filteredMembers = TeamMembers
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
            TeamMembers.Add(newMember);
            filteredMembers.Add(newMember);
            TeamList.Items.Refresh();
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

            var result = MessageBox.Show($"Сигурни ли сте, че искате да премахнете {selected.Name}?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                TeamMembers.Remove(selected);
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
