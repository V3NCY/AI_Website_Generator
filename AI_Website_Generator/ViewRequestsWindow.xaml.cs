using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

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
                    MessageBox.Show("Файлът requests.json не е намерен:\n" + jsonPath,
                        "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Requests.Clear();
                    return;
                }

                string json = File.ReadAllText(jsonPath);
                if (!json.TrimStart().StartsWith("["))
                {
                    MessageBox.Show("Невалиден JSON формат в requests.json.",
                        "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }

        private void RequestsList_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest)
            {
                ContextMenu menu = new ContextMenu();
                string[] statuses = {
                    "Получена заявка", "В процес", "За дизайнер",
                    "За начално тестване", "За финално тестване",
                    "За технически екип", "Проблем", "Онлайн"
                };

                foreach (string status in statuses)
                {
                    MenuItem item = new MenuItem { Header = status };
                    item.Click += ChangeStatus_Click;
                    menu.Items.Add(item);
                }

                menu.IsOpen = true;
            }
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsList.SelectedItem is Request request && sender is MenuItem item)
            {
                request.Status = item.Header.ToString();
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
                    string templateKey = request.Template?.Trim();

                    // Map numbers to letters
                    switch (templateKey)
                    {
                        case "1":
                            templateKey = "A";
                            break;
                        case "2":
                            templateKey = "B";
                            break;
                        case "3":
                            templateKey = "C";
                            break;
                        case "A":
                        case "B":
                        case "C":
                            break;
                        default:
                            MessageBox.Show("Невалиден темплейт: " + request.Template,
                                "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }

                    string htmlFile = $"template{templateKey}.html";
                    string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", htmlFile);
                    string zipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", $"template{templateKey}.zip");

                    if (!File.Exists(htmlPath))
                    {
                        MessageBox.Show($"Липсва HTML файл: {htmlFile}",
                            "Липсващ файл", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (!File.Exists(zipPath))
                    {
                        CreateWordPressThemeZip(htmlPath, zipPath, $"Template {templateKey}");
                    }

                    string siteFolder = request.NewDomain.Replace(".", "").Replace("www", "");
                    string targetDir = Path.Combine(@"C:\xampp\htdocs", siteFolder);

                    if (Directory.Exists(targetDir))
                        Directory.Delete(targetDir, true);

                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, targetDir);

                    MessageBox.Show($"Темплейтът е деплойнат успешно в:\n{targetDir}",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = $"http://localhost/{siteFolder}",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при деплой:\n" + ex.Message,
                        "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void CreateWordPressThemeZip(string htmlPath, string outputZipPath, string themeName)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), $"wp_theme_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempDir);

            string html = File.ReadAllText(htmlPath);
            string indexPhp = html.Replace("<!DOCTYPE html>", "<?php /* Generated Template */ ?>\n<!DOCTYPE html>");
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
