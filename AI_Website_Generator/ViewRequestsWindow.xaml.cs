using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

        public ObservableCollection<Request> Requests { get; set; } = new ObservableCollection<Request>();

        private void LoadRequests()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");

                if (!File.Exists(jsonPath))
                {
                    MessageBox.Show("Файлът requests.json не е намерен в директорията:\n" + jsonPath,
                                    "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Requests.Clear(); // Clear current collection
                    return;
                }

                string json = File.ReadAllText(jsonPath);

                if (!json.TrimStart().StartsWith("["))
                {
                    MessageBox.Show("Файлът requests.json не съдържа валиден масив от заявки.",
                                    "Грешка при формата", MessageBoxButton.OK, MessageBoxImage.Error);
                    Requests.Clear();
                    return;
                }

                var requestsList = JsonConvert.DeserializeObject<List<Request>>(json);
                Requests.Clear();
                foreach (var req in requestsList ?? new List<Request>())
                    Requests.Add(req);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на заявките:\n" + ex.Message,
                                "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                Requests.Clear();
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
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest && sender is MenuItem menuItem)
            {
                selectedRequest.Status = menuItem.Header.ToString();
                RequestsList.Items.Refresh();

                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(Requests, Formatting.Indented));
            }
        }

        private void DeployTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Request request)
            {
                try
                {
                    string templateFile = $"template{request.Template}.zip";
                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", templateFile);
                    string siteFolder = request.NewDomain.Replace(".", "").Replace("www", "");
                    string targetDir = Path.Combine(@"C:\xampp\htdocs", siteFolder);

                    if (!File.Exists(templatePath))
                    {
                        string htmlFile = $"template{request.Template}.html";
                        string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", htmlFile);

                        if (!File.Exists(htmlPath))
                        {
                            MessageBox.Show($"Няма HTML файл за темплейт {request.Template}", "Липсващ файл", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        CreateWordPressThemeZip(htmlPath, templatePath, $"Template {request.Template}");
                    }


                    if (Directory.Exists(targetDir))
                        Directory.Delete(targetDir, true);

                    System.IO.Compression.ZipFile.ExtractToDirectory(templatePath, targetDir);
                    MessageBox.Show($"Темплейтът е успешно деплойнат в:\n{targetDir}", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = $"http://localhost/{siteFolder}",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Възникна грешка при деплой:\n" + ex.Message, "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        void CreateWordPressThemeZip(string htmlPath, string outputZipPath, string themeName)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), $"wp_theme_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempDir);

            string htmlContent = File.ReadAllText(htmlPath);

            string indexPhp = htmlContent
                .Replace("<!DOCTYPE html>", "<?php /* Template generated */ ?>\n<!DOCTYPE html>"); 

            File.WriteAllText(Path.Combine(tempDir, "index.php"), indexPhp);

            string styleCss = $@"/*
            Theme Name: {themeName}
            Author: Orak Academy
            Version: 1.0
            */";
            File.WriteAllText(Path.Combine(tempDir, "style.css"), styleCss);

            if (File.Exists(outputZipPath)) File.Delete(outputZipPath);
            System.IO.Compression.ZipFile.CreateFromDirectory(tempDir, outputZipPath);
            Directory.Delete(tempDir, true);
        }




    }
}