using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;

namespace AI_Website_Generator
{
    public partial class TeamManagementWindow : Window
    {
        private static readonly string TeamFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "team.json");

        public static ObservableCollection<TeamMember> TeamMembers { get; set; } = new();

        private ICollectionView _view;
        private string _roleFilter = null;

        private bool isNameAscending = true;
        private bool isRoleAscending = true;

        public TeamManagementWindow()
        {
            InitializeComponent();
            LoadTeamMembers();

            _view = CollectionViewSource.GetDefaultView(TeamMembers);
            _view.Filter = FilterTeam;

            TeamList.ItemsSource = _view;
        }

        private bool FilterTeam(object obj)
        {
            if (obj is not TeamMember t) return false;

            var q = (SearchBox.Text ?? "").Trim().ToLower();

            bool matchesSearch =
                string.IsNullOrWhiteSpace(q) ||
                (t.Name ?? "").ToLower().Contains(q) ||
                (t.Role ?? "").ToLower().Contains(q);

            bool matchesRole =
                string.IsNullOrWhiteSpace(_roleFilter) ||
                string.Equals(t.Role, _roleFilter, StringComparison.OrdinalIgnoreCase);

            return matchesSearch && matchesRole;
        }

        private void LoadTeamMembers()
        {
            TeamMembers.Clear();

            if (File.Exists(TeamFilePath))
            {
                try
                {
                    var json = File.ReadAllText(TeamFilePath);
                    var loaded = JsonSerializer.Deserialize<ObservableCollection<TeamMember>>(json);

                    if (loaded != null && loaded.Count > 0)
                    {
                        foreach (var m in loaded)
                            TeamMembers.Add(m);
                        return;
                    }
                }
                catch { }
            }

            LoadDefaultTeam();
        }


        private void LoadDefaultTeam()
        {
            TeamMembers.Clear();
            TeamMembers.Add(new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" });
            TeamMembers.Add(new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" });
            TeamMembers.Add(new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" });
            TeamMembers.Add(new TeamMember { Name = "Katya Kalcheva", Role = "QA Tester" });
            TeamMembers.Add(new TeamMember { Name = "Yordan Totev", Role = "Seller" });

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

        private void Search_Click(object sender, RoutedEventArgs e) => _view.Refresh();

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // optional: re-load from disk
            LoadTeamMembers();
            _view = CollectionViewSource.GetDefaultView(TeamMembers);
            _view.Filter = FilterTeam;
            TeamList.ItemsSource = _view;
            _view.Refresh();
        }

        private void AddTeamMember_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Въведете името:", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(name)) return;

            string role = Microsoft.VisualBasic.Interaction.InputBox("Въведете роля:", "Добавяне на член");
            if (string.IsNullOrWhiteSpace(role)) return;

            TeamMembers.Add(new TeamMember { Name = name.Trim(), Role = role.Trim() });
            SaveTeamMembers();
            _view.Refresh();
        }

        private void EditTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is TeamMember selected)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Редактирай името:", "Редакция", selected.Name);
                string newRole = Microsoft.VisualBasic.Interaction.InputBox("Редактирай ролята:", "Редакция", selected.Role);

                if (!string.IsNullOrWhiteSpace(newName)) selected.Name = newName.Trim();
                if (!string.IsNullOrWhiteSpace(newRole)) selected.Role = newRole.Trim();

                SaveTeamMembers();
                _view.Refresh();
            }
        }

        private void RemoveTeamMember_Click(object sender, RoutedEventArgs e)
        {
            if (TeamList.SelectedItem is TeamMember selected)
            {
                var result = MessageBox.Show($"Сигурни ли сте, че искате да премахнете {selected.Name}?",
                                             "Потвърждение", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    TeamMembers.Remove(selected);
                    SaveTeamMembers();
                    _view.Refresh();
                }
            }
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            _view.SortDescriptions.Clear();
            _view.SortDescriptions.Add(new SortDescription(nameof(TeamMember.Name),
                isNameAscending ? ListSortDirection.Ascending : ListSortDirection.Descending));
            isNameAscending = !isNameAscending;
        }

        private void SortByRole_Click(object sender, RoutedEventArgs e)
        {
            _view.SortDescriptions.Clear();
            _view.SortDescriptions.Add(new SortDescription(nameof(TeamMember.Role),
                isRoleAscending ? ListSortDirection.Ascending : ListSortDirection.Descending));
            isRoleAscending = !isRoleAscending;
        }

        // Role filter buttons
        private void FilterAll_Click(object sender, RoutedEventArgs e) { _roleFilter = null; _view.Refresh(); }
        private void FilterDesigner_Click(object sender, RoutedEventArgs e) { _roleFilter = "Designer"; _view.Refresh(); }
        private void FilterPicker_Click(object sender, RoutedEventArgs e) { _roleFilter = "Request Picker"; _view.Refresh(); }
        private void FilterTech_Click(object sender, RoutedEventArgs e) { _roleFilter = "Tech Team"; _view.Refresh(); }
        private void FilterTester_Click(object sender, RoutedEventArgs e) { _roleFilter = "Tester"; _view.Refresh(); }
        private void FilterSeller_Click(object sender, RoutedEventArgs e) { _roleFilter = "Seller"; _view.Refresh(); }
    }

    public class TeamMember : INotifyPropertyChanged
    {
        private string _name;
        private string _role;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Role { get => _role; set { _role = value; OnPropertyChanged(nameof(Role)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
