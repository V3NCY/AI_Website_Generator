using System.Windows;
using System.Windows.Input;

namespace AI_Website_Generator.user
{
    public partial class PasswordChangeDialog : Window
    {
        public string NewPassword { get; private set; }

        public PasswordChangeDialog(string username)
        {
            InitializeComponent();
            Title = $"Смяна на парола: {username}";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            NewPassword = NewPasswordBox.Password.Trim();
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                MessageBox.Show("Моля, въведете нова парола.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
