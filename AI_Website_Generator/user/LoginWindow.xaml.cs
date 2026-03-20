using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Orak.WebPro.Admin.user
{
    public partial class LoginWindow : Window
    {
        private readonly string _loginFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user", "logins.json");

        private List<AppUser> _users;

        public string LoggedInUser { get; private set; }
        public string LoggedInRole { get; private set; }

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
                    _users = JsonSerializer.Deserialize<List<AppUser>>(json) ?? new List<AppUser>();
                }
                else
                {
                    _users = new List<AppUser>
                    {
                        new AppUser
                        {
                            Username = "venceslava.georgieva",
                            Password = "ven112233g",
                            Role = "Admin"
                        },
                        new AppUser
                        {
                            Username = "elvira.shugova",
                            Password = "elv1r@sh",
                            Role = "Support"
                        },
                        new AppUser
                        {
                            Username = "denka.arabadzhiyska",
                            Password = "d3nkk@333",
                            Role = "Team"
                        },
                        new AppUser
                        {
                            Username = "admin.ven",
                            Password = "admin123@",
                            Role = "Admin"
                        }
                    };

                    File.WriteAllText(
                        _loginFilePath,
                        JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true })
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Грешка при зареждане на потребителите: {ex.Message}",
                    "Грешка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                _users = new List<AppUser>();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text.Trim().ToLower();
            string password = LoginPassword.Password.Trim();

            var user = _users.FirstOrDefault(u =>
                u.Username.ToLower() == username &&
                u.Password == password);

            if (user != null)
            {
                LoggedInUser = user.Username;
                LoggedInRole = user.Role;


                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(
                    "Невалидни потребителско име или парола.",
                    "Грешка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "Моля, въведете потребителско име, за да смените паролата.",
                    "Информация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            var user = _users.FirstOrDefault(u => u.Username.ToLower() == username);

            if (user != null)
            {
                var inputDialog = new PasswordChangeDialog(username);

                if (inputDialog.ShowDialog() == true)
                {
                    user.Password = inputDialog.NewPassword;

                    File.WriteAllText(
                        _loginFilePath,
                        JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true })
                    );

                    MessageBox.Show(
                        "Паролата е обновена успешно!",
                        "Успех",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(
                    "Потребителят не е намерен.",
                    "Грешка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login_Click(sender, e);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}