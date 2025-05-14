using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AI_Website_Generator
{
    public partial class AddIssueWindow : Window
    {
        public AddIssueWindow()
        {
            InitializeComponent();
        }
        private List<TechIssue> issueList;

        public AddIssueWindow(List<TechIssue> issues)
        {
            InitializeComponent();
            issueList = issues; 
        }
        private void AddIssue_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IssueText.Text) &&
                !string.IsNullOrWhiteSpace(AssignedToText.Text) &&
                !string.IsNullOrWhiteSpace(CreatedByText.Text))
            {
                issueList.Add(new TechIssue
                {
                    Issue = IssueText.Text,
                    AssignedTo = AssignedToText.Text,
                    Status = "Очаква",
                    LastUpdatedBy = CreatedByText.Text
                });

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Моля, попълнете всички полета.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
                if (textBox.Name == "IssueText") textBox.Text = "Описание на проблема";
                else if (textBox.Name == "AssignedToText") textBox.Text = "Отговорник";
                else if (textBox.Name == "CreatedByText") textBox.Text = "Вашето име";
                else if (textBox.Name == "UpdatedBy") textBox.Text = "Вашето име";

                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
