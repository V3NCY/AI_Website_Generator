using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AI_Website_Generator
{
    public partial class ViewRequestsWindow : Window
    {
        public ViewRequestsWindow()
        {
            InitializeComponent();
            LoadRequests();
            DataContext = this;
        }
        public ObservableCollection<Request> Requests { get; set; }
        private void LoadRequests()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");

                if (!File.Exists(jsonPath))
                {
                    MessageBox.Show("Файлът requests.json не е намерен в директорията:\n" + jsonPath,
                                    "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Requests = new ObservableCollection<Request>();
                    return;
                }

                string json = File.ReadAllText(jsonPath);

                if (!json.TrimStart().StartsWith("["))
                {
                    MessageBox.Show("Файлът requests.json не съдържа валиден масив от заявки.",
                                    "Грешка при формата", MessageBoxButton.OK, MessageBoxImage.Error);
                    Requests = new ObservableCollection<Request>();
                    return;
                }

                var requestsList = JsonConvert.DeserializeObject<List<Request>>(json);
                Requests = new ObservableCollection<Request>(requestsList ?? new List<Request>());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на заявките:\n" + ex.Message,
                                "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                Requests = new ObservableCollection<Request>();
            }
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
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
            RequestsList.Items.Refresh();
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
