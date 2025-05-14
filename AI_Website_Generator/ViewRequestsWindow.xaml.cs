using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class ViewRequestsWindow : Window
    {
        public ViewRequestsWindow()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            var requests = new List<Request>();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var r = JsonSerializer.Deserialize<Request>(line);
                    if (r != null) requests.Add(r);
                }
            }

            RequestsList.ItemsSource = requests;
        }


        private void RequestsList_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest)
            {
                ContextMenu menu = new ContextMenu();
                string[] statusOptions = { "Получена заявка", "В процес", "За дизайнер", "За начално тестване", "За финално тестване", "За технически екип", "Проблем", "Онлайн" };

                foreach (string status in statusOptions)
                {
                    MenuItem menuItem = new MenuItem { Header = status };
                    menuItem.Click += ChangeStatus_Click;
                    menu.Items.Add(menuItem);
                }

                menu.IsOpen = true;
            }
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest && sender is MenuItem menuItem)
            {
                selectedRequest.Status = menuItem.Header.ToString();
                RequestsList.Items.Refresh();
            }
        }
    }
}
