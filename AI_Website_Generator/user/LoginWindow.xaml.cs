using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace AI_Website_Generator.user
{
    public partial class LoginWindow : Window
    {
        public string LoggedInUser { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private readonly Dictionary<string, string> validUsers = new Dictionary<string, string>
        {
            { "venceslava.georgieva", "ven112233g" },
            { "elvira.shugova", "elv1r@sh" },
            { "denka.arabadzhiyska", "d3nkk@333" }
        };

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim().ToLower();
            string pass = txtPassword.Password.Trim();

            if (validUsers.ContainsKey(user) && validUsers[user] == pass)
            {
                LoggedInUser = user;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Невалидни потребителско име или парола.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
