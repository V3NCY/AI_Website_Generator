using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace AI_Website_Generator.user
{
    public partial class LoginWindow : Window
    {
        private readonly string _loginFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logins.json");
        private Dictionary<string, string> _users;

        public string LoggedInUser { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                if (File.Exists(_loginFilePath))
                {
                    var json = File.ReadAllText(_loginFilePath);
                    _users = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
                }
                else
                {
                    // Default users if file doesn't exist
                    _users = new Dictionary<string, string>
                    {
                        { "venceslava.georgieva", "ven112233g" },
                        { "elvira.shugova", "elv1r@sh" },
                        { "denka.arabadzhiyska", "d3nkk@333" }
                    };
                    File.WriteAllText(_loginFilePath, JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при зареждане на потребителите: {ex.Message}", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                _users = new();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = LoginUsername.Text.Trim().ToLower();
            string pass = LoginPassword.Password.Trim();

            if (_users.ContainsKey(user) && _users[user] == pass)
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

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            string user = LoginUsername.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Моля, въведете потребителско име, за да смените паролата.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_users.ContainsKey(user))
            {
                var inputDialog = new PasswordChangeDialog(user);
                if (inputDialog.ShowDialog() == true)
                {
                    string newPassword = inputDialog.NewPassword;

                    _users[user] = newPassword;
                    File.WriteAllText(_loginFilePath, JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true }));

                    MessageBox.Show("Паролата е обновена успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Потребителят не е намерен.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Login_Click(sender, e);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}
