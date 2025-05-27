using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class TeamManagementWindow : Window
    {
        private static readonly string TeamFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "team.json");
        public static ObservableCollection<TeamMember> TeamMembers { get; set; } = new ObservableCollection<TeamMember>();
        private bool isNameAscending = true;
        private bool isRoleAscending = true;

        public TeamManagementWindow()
        {
            InitializeComponent();
            LoadTeamMembers();
            TeamList.ItemsSource = TeamMembers;
        }

        private void LoadTeamMembers()
        {
            if (File.Exists(TeamFilePath))
            {
                try
                {
                    var json = File.ReadAllText(TeamFilePath);
                    var loaded = JsonSerializer.Deserialize<ObservableCollection<TeamMember>>(json);
                    if (loaded != null && loaded.Count > 0)
                        TeamMembers = loaded;
                }
                catch
                {
                    LoadDefaultTeam();
                }
            }
            else
            {
                LoadDefaultTeam();
            }
        }

        private void LoadDefaultTeam()
        {
            TeamMembers = new ObservableCollection<TeamMember>
            {
                new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" },
                new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" },
                new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" },
                new TeamMember { Name = "Katya Kalcheva", Role = "Tester" },
                new TeamMember { Name = "Yordan Totev", Role = "Seller" }
            };
            SaveTeamMembers();
        }

        private void SaveTeamMembers()
        {
            try
            {
                var json = JsonSerializer.Serialize(TeamMembers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(TeamFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при запазване на екипа:\n" + ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(query))
                TeamList.ItemsSource = TeamMembers;
            else
                TeamList.ItemsSource = TeamMembers.Where(t => t.Name.ToLower().Contains(query) || t.Role.ToLower().Contains(query)).ToList();
        }

        private void AddTeamMember_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Въведете името:", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(name)) return;

            string role = Microsoft.VisualBasic.Interaction.InputBox("Въведете роля:", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(role)) return;

            TeamMembers.Add(new TeamMember { Name = name.Trim(), Role = role.Trim() });
            TeamList.ItemsSource = TeamMembers;
            SaveTeamMembers();
        }

        private void EditTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is TeamMember selected)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Редактирай името:", "Редакция", selected.Name);
                string newRole = Microsoft.VisualBasic.Interaction.InputBox("Редактирай ролята:", "Редакция", selected.Role);

                if (!string.IsNullOrWhiteSpace(newName)) selected.Name = newName.Trim();
                if (!string.IsNullOrWhiteSpace(newRole)) selected.Role = newRole.Trim();

                TeamList.Items.Refresh();
                SaveTeamMembers();
            }
        }

        private void RemoveTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is TeamMember selected)
            {
                var result = MessageBox.Show($"Сигурни ли сте, че искате да премахнете {selected.Name}?", "Потвърждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    TeamMembers.Remove(selected);
                    TeamList.ItemsSource = TeamMembers;
                    SaveTeamMembers();
                }
            }
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ItemsSource = isNameAscending
                ? TeamMembers.OrderBy(t => t.Name).ToList()
                : TeamMembers.OrderByDescending(t => t.Name).ToList();
            isNameAscending = !isNameAscending;
        }

        private void SortByRole_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ItemsSource = isRoleAscending
                ? TeamMembers.OrderBy(t => t.Role).ToList()
                : TeamMembers.OrderByDescending(t => t.Role).ToList();
            isRoleAscending = !isRoleAscending;
        }
    }

    public class TeamMember
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
