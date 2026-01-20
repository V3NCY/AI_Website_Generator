using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class ManageDesignsWindow : Window
    {
        public ManageDesignsWindow()
        {
            InitializeComponent();
            UpdateSelectedUI(null);
        }

        // Keep the relative path from Tag, but always resolve safely to full path
        private string? GetSelectedFullPath()
        {
            if (TemplateList.SelectedItem is ListBoxItem item && item.Tag is string relPath)
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string full = Path.GetFullPath(Path.Combine(baseDir, relPath));
                return full;
            }
            return null;
        }

        private void TemplateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TemplateList.SelectedItem is ListBoxItem item)
            {
                string name = item.Content?.ToString() ?? "(неизвестно)";
                string? fullPath = GetSelectedFullPath();

                UpdateSelectedUI(name, fullPath);
            }
            else
            {
                UpdateSelectedUI(null);
            }
        }

        private void UpdateSelectedUI(string? name, string? fullPath = null)
        {
            txtSelectedName.Text = string.IsNullOrWhiteSpace(name) ? "(не е избран)" : name;
            txtSelectedPath.Text = fullPath ?? "";

            if (string.IsNullOrWhiteSpace(fullPath))
            {
                txtStatus.Text = "Избери темплейт отляво, за да видиш информация и действия.";
                return;
            }

            if (File.Exists(fullPath))
            {
                txtStatus.Text = "✅ Файлът е наличен. Можеш да го отвориш или да отвориш папката.";
            }
            else
            {
                txtStatus.Text = "⚠️ Файлът НЕ е намерен. Провери дали съществува в папка /templates.";
            }
        }

        private void OpenSelected_Click(object sender, RoutedEventArgs e)
        {
            string? fullPath = GetSelectedFullPath();
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                MessageBox.Show("Моля избери темплейт.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Template not found:\n" + fullPath, "Missing file", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening template:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            string? fullPath = GetSelectedFullPath();
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                MessageBox.Show("Моля избери темплейт.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                string folder = Path.GetDirectoryName(fullPath) ?? AppDomain.CurrentDomain.BaseDirectory;

                if (!Directory.Exists(folder))
                {
                    MessageBox.Show("Folder not found:\n" + folder, "Missing folder", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = folder,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening folder:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CopyPath_Click(object sender, RoutedEventArgs e)
        {
            string? fullPath = GetSelectedFullPath();
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                MessageBox.Show("Няма избран темплейт.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Clipboard.SetText(fullPath);
            txtStatus.Text = "📋 Пътят е копиран в clipboard.";
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // You currently have static list items in XAML.
            // This refresh just re-evaluates the selected file status.
            if (TemplateList.SelectedItem is ListBoxItem item)
            {
                string name = item.Content?.ToString() ?? "(неизвестно)";
                string? fullPath = GetSelectedFullPath();
                UpdateSelectedUI(name, fullPath);
            }
            else
            {
                UpdateSelectedUI(null);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // simple UI-only filter (keeps your 3 items, just hides non-matching)
            string q = (txtSearch.Text ?? "").Trim().ToLowerInvariant();

            foreach (var obj in TemplateList.Items)
            {
                if (obj is ListBoxItem item)
                {
                    string text = (item.Content?.ToString() ?? "").ToLowerInvariant();
                    item.Visibility = string.IsNullOrWhiteSpace(q) || text.Contains(q)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }
            }
        }
    }
}
