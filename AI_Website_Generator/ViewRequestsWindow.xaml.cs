using System;
using System.Collections.Generic;
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
            var requests = new List<Request>
            {
                new Request { Client = "Иван Петров", Phone = "0888123456", Email = "ivan.petrov@email.com", Code = "12345", Institute = "ОУ 'Христо Ботев'", City = "София", PrevDomain = "hristobotev.bg", NewDomain = "ouhristobotev.bg", Status = "За дизайнер" },
                new Request { Client = "Мария Иванова", Phone = "0899112233", Email = "maria.ivanova@email.com", Code = "67890", Institute = "Гимназия 'Васил Левски'", City = "Пловдив", PrevDomain = "vlevsky.com", NewDomain = "vasilevski.bg", Status = "В процес" },
                new Request { Client = "Георги Димитров", Phone = "0877766554", Email = "georgi.dimitrov@email.com", Code = "54321", Institute = "Технически университет", City = "Варна", PrevDomain = "techvarna.net", NewDomain = "techvarna.bg", Status = "Получена заявка" },
                new Request { Client = "Теодора Шопова", Phone = "0878212314", Email = "teodora.shopova@email.com", Code = "134521", Institute = "ОУ 'Д-р Петър Берон'", City = "Ямбол", PrevDomain = "oyberon.net", NewDomain = "oupetarberon.bg", Status = "Избран темплейт" }
            };

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
