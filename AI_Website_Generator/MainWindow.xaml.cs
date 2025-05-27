using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AI_Website_Generator.user;

namespace AI_Website_Generator
{
    public partial class MainWindow : Window
    {
        public string CurrentUsername { get; set; } = "Гост";
        private readonly string chatFile = "chatlog.txt";
        private LocalWebServer _webServer;

        public MainWindow()
        {
            var login = new LoginWindow();
            bool? result = login.ShowDialog();

            if (result != true)
            {
                Application.Current.Shutdown();
                return;
            }

            CurrentUsername = login.LoggedInUser;
            InitializeComponent();
            DataContext = this;

            _webServer = new LocalWebServer(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates"), 8000);
            _webServer.Start();
            RefreshChat_Click(null, null);
        }

        private void OpenAddDomainWindow_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<AddDomainWindow>();
        }

        private void AddNewDomain_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDomainWindow();
            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                Domain newDomain = addWindow.NewDomain;
                Domain.AddDomain(newDomain); 

                MessageBox.Show($"Успешно добавен домейн:\n{newDomain.NewDomainName}",
                                "Нов уебсайт", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<ViewRequestsWindow>();
        }

        private void btnManageDesigns_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<ManageDesignsWindow>();
        }

        private void btnTechSupport_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<TechnicalSupportWindow>();
        }

        private void btnManageTeam_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<TeamManagementWindow>();
        }

        private void OpenWebsiteStatistics_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<WebsiteStatisticsWindow>();
        }

        private void OpenDomainList_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow<DomainListWindow>();
        }

        private void OpenChatbotWindow_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:8000/ChatBotLink.html";

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при отваряне на чатбота:\n" + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данните са обновени успешно!", "Обновяване", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshChat_Click(object sender, RoutedEventArgs e)
        {
            if (this.FindName("ChatMessages") is ItemsControl chatList)
            {
                chatList.Items.Clear();

                if (File.Exists(chatFile))
                {
                    var lines = File.ReadAllLines(chatFile);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var message = new ChatMessage
                        {
                            Message = lines[i],
                            Time = "",
                            IsMe = lines[i].Contains(CurrentUsername),
                            IsLatest = (i == lines.Length - 1)
                        };
                        chatList.Items.Add(message);
                    }
                }
            }
        }

        private void SendMessage()
        {
            if (this.FindName("ChatInput") is TextBox input && this.FindName("ChatMessages") is ItemsControl chatList)
            {
                string user = "👤 " + CurrentUsername;
                string time = DateTime.Now.ToString("HH:mm");
                string message = input.Text.Trim();

                if (!string.IsNullOrEmpty(message))
                {
                    string fullMessage = $"{time} {user}: {message}";
                    File.AppendAllLines(chatFile, new[] { fullMessage });
                    input.Clear();
                    RefreshChat_Click(null, null);
                }
            }
        }

        private void SendChatMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void OpenWindow<T>() where T : Window, new()
        {
            var existing = Application.Current.Windows.OfType<T>().FirstOrDefault();
            if (existing == null)
            {
                var window = new T();
                window.Show();
            }
            else
            {
                existing.Focus();
            }
        }
    }
}
