using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AI_Website_Generator
{
    public partial class ChangeStatusWindow : Window
    {
        private TechIssue issue;

        public ChangeStatusWindow(TechIssue selectedIssue)
        {
            InitializeComponent();
            issue = selectedIssue;
        }
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Вашето име";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (StatusDropdown.SelectedItem is ComboBoxItem selectedStatus && !string.IsNullOrWhiteSpace(UpdatedBy.Text))
            {
                issue.Status = selectedStatus.Content.ToString();
                issue.LastUpdatedBy = UpdatedBy.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Моля, изберете статус и въведете вашето име.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public class TechIssue
        {
            public string Issue { get; set; }
            public string AssignedTo { get; set; }
            public string Status { get; set; }
            public string Priority { get; set; }
            public string Category { get; set; }
            public string ClientName { get; set; }
            public string WebsiteDomain { get; set; }
            public string CreatedDate { get; set; } = System.DateTime.Now.ToString("dd.MM.yyyy");
            public string LastUpdatedBy { get; set; } = "Системата"; // <-- Add this line back
        }

    }
}
