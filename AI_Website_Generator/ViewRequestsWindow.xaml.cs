using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

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

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                    string templateKey = request.Template?.Trim()?.ToUpper();

                    switch (templateKey)
                    {
                        case "1": templateKey = "A"; break;
                        case "2": templateKey = "B"; break;
                        case "3": templateKey = "C"; break;
                        case "A":
                        case "B":
                        case "C": break;
                        default:
                            MessageBox.Show("Невалиден темплейт: " + request.Template,
                                "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }

                    string htmlFile = $"template{templateKey}.html";
                    string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", htmlFile);
                    string outputZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", $"template{templateKey}.zip");
                    string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DeployedTemplates", $"template{templateKey}");

                    if (!File.Exists(htmlPath))
                    {
                        MessageBox.Show($"Липсва HTML файл: {htmlFile}",
                            "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    CreateWordPressThemeZip(htmlPath, outputZipPath, $"Template {templateKey}");

                    if (Directory.Exists(targetFolder))
                    {
                        try { Directory.Delete(targetFolder, true); }
                        catch (Exception delEx)
                        {
                            MessageBox.Show("Грешка при изтриване на предишната версия:\n" + delEx.Message,
                                "Файлова грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    System.IO.Compression.ZipFile.ExtractToDirectory(outputZipPath, targetFolder);

                    string finalIndexPath = Path.Combine(targetFolder, "index.php");

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = finalIndexPath,
                        UseShellExecute = true
                    });

                    MessageBox.Show($"Template {templateKey} е успешно деплойнат.\n\nПът: {targetFolder}\nФайл: {finalIndexPath}",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

            try
            {
                string bodyHtml = File.ReadAllText(htmlPath);

                string indexPhp = $@"<?php
                                    /* Template: {themeName} */
                                    ?><!DOCTYPE html>
                                    <html>
                                    <head>
                                        <meta charset='UTF-8'>
                                        <title>{themeName}</title>
                                    </head>
                                    <body>
                                    {bodyHtml}
                                    </body>
                                    </html>";

                File.WriteAllText(Path.Combine(tempDir, "index.php"), indexPhp);

                string styleCss = $@"/*
                Theme Name: {themeName}
                Author: Orak Academy
                Version: 1.0
                */";
                File.WriteAllText(Path.Combine(tempDir, "style.css"), styleCss);

                if (File.Exists(outputZipPath)) File.Delete(outputZipPath);
                System.IO.Compression.ZipFile.CreateFromDirectory(tempDir, outputZipPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при създаване на темплейта:\n" + ex.Message,
                    "ZIP грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                try { Directory.Delete(tempDir, true); } catch { }
            }
        }

        private void RequestsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest)
            {
                string[] editableStatuses = {
                    "Проблем", "За дизайнер", "За технически екип", "В процес", "Получена заявка"
                };

                if (Array.Exists(editableStatuses, s => s == selectedRequest.Status))
                {
                    string input = Interaction.InputBox(
                        $"Добави/редактирай коментар за статус: {selectedRequest.Status}",
                        "Коментар за заявката",
                        selectedRequest.Comment ?? "");

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        selectedRequest.Comment = input.Trim();
                        RequestsList.Items.Refresh();

                        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");
                        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(Requests, Formatting.Indented));
                    }
                }
                else
                {
                    MessageBox.Show("Коментари могат да се добавят само при определени статуси.",
                        "Неразрешена операция", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
