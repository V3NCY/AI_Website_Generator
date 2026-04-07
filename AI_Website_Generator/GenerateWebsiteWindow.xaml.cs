using System;
using System.IO;
using System.Windows;
using Media = System.Windows.Media;
using WinForms = System.Windows.Forms;
using Orak.WebPro.Services.Services;
using Orak.WebPro.Shared.DTOs;
using Orak.WebPro.Shared.Enums;

namespace Orak.WebPro.Admin
{
    public partial class GenerateWebsiteWindow : Window
    {
        private readonly int _websiteId;

        public GenerateWebsiteWindow(int websiteId = 0)
        {
            InitializeComponent();
            _websiteId = websiteId;
            cmbTemplate.SelectedIndex = 0;
        }

        public void PreFill(
            string name, string city, string email,
            string phone, string address, string director,
            string domain, string neispuoCode,
            InstitutionType type)
        {
            txtName.Text = name ?? string.Empty;
            txtCity.Text = city ?? string.Empty;
            txtEmail.Text = email ?? string.Empty;
            txtPhone.Text = phone ?? string.Empty;
            txtAddress.Text = address ?? string.Empty;
            txtDirector.Text = director ?? string.Empty;
            txtDomain.Text = domain ?? string.Empty;

            switch (type)
            {
                case InstitutionType.University:
                    cmbTemplate.SelectedIndex = 1;
                    break;
                case InstitutionType.Kindergarten:
                    cmbTemplate.SelectedIndex = 2;
                    break;
                default:
                    cmbTemplate.SelectedIndex = 0;
                    break;
            }
        }

        private async void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            btnGenerate.IsEnabled = false;
            txtStatus.Text = "⏳ Генерирам сайт...";
            txtStatus.Foreground = new Media.SolidColorBrush(Media.Color.FromRgb(52, 152, 219));

            try
            {
                var dto = BuildDto();

                string templatesPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "templates");

                string outputPath = txtOutputFolder.Text.Trim();

                var generator = new WebsiteGeneratorService(templatesPath, outputPath);
                var result = await generator.GenerateAsync(dto);

                if (result.Success)
                {
                    txtStatus.Text = $"✅ {result.Message}";
                    txtStatus.Foreground = new Media.SolidColorBrush(Media.Color.FromRgb(39, 174, 96));

                    var answer = System.Windows.MessageBox.Show(
                        $"Сайтът е генериран успешно!\n\nПапка: {result.SiteFolder}\n\nОтвори папката?",
                        "Успех!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);

                    if (answer == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", result.SiteFolder);
                    }
                }
                else
                {
                    txtStatus.Text = $"❌ {result.Message}";
                    txtStatus.Foreground = new Media.SolidColorBrush(Media.Color.FromRgb(192, 57, 43));
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"❌ Грешка: {ex.Message}";
                txtStatus.Foreground = new Media.SolidColorBrush(Media.Color.FromRgb(192, 57, 43));
            }
            finally
            {
                btnGenerate.IsEnabled = true;
            }
        }

        private GenerateWebsiteDto BuildDto()
        {
            var tplItem = cmbTemplate.SelectedItem as System.Windows.Controls.ComboBoxItem;

            InstitutionType type;
            switch (tplItem != null ? tplItem.Tag?.ToString() : null)
            {
                case "University":
                    type = InstitutionType.University;
                    break;
                case "Kindergarten":
                    type = InstitutionType.Kindergarten;
                    break;
                default:
                    type = InstitutionType.School;
                    break;
            }

            int founded;
            int students;

            int.TryParse(txtFounded != null ? txtFounded.Text.Trim() : string.Empty, out founded);
            int.TryParse(txtStudents != null ? txtStudents.Text.Trim() : string.Empty, out students);

            return new GenerateWebsiteDto
            {
                WebsiteId = _websiteId,
                InstitutionName = txtName.Text.Trim(),
                City = txtCity.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                DirectorName = txtDirector.Text.Trim(),
                Domain = txtDomain.Text.Trim(),
                Motto = txtMotto != null ? txtMotto.Text.Trim() : string.Empty,
                FoundedYear = founded,
                StudentsCount = students,
                TemplateType = type,
                PrimaryColor = txtColor.Text.Trim()
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Въведете наименование на институцията.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                ShowError("Въведете населено място.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowError("Въведете имейл адрес.");
                return false;
            }

            if (cmbTemplate.SelectedIndex < 0)
            {
                ShowError("Изберете шаблон.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOutputFolder.Text))
            {
                ShowError("Изберете папка за изход.");
                return false;
            }

            return true;
        }

        private void ShowError(string msg)
        {
            txtStatus.Text = $"⚠️ {msg}";
            txtStatus.Foreground = new Media.SolidColorBrush(Media.Color.FromRgb(192, 57, 43));
        }

        private void ColorPreview_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using (var dlg = new WinForms.ColorDialog())
            {
                if (dlg.ShowDialog() == WinForms.DialogResult.OK)
                {
                    var c = dlg.Color;
                    string hex = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                    txtColor.Text = hex;
                    colorPreview.Background = new Media.SolidColorBrush(Media.Color.FromRgb(c.R, c.G, c.B));
                }
            }
        }

        private void txtColor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                string hex = txtColor.Text.Trim();
                if (hex.StartsWith("#") && hex.Length == 7)
                {
                    var brush = new Media.BrushConverter().ConvertFrom(hex) as Media.SolidColorBrush;
                    if (brush != null)
                    {
                        colorPreview.Background = brush;
                    }
                }
            }
            catch
            {
            }
        }

        private void BrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            using (var dlg = new WinForms.FolderBrowserDialog())
            {
                dlg.Description = "Изберете папка за генерираните сайтове";
                dlg.ShowNewFolderButton = true;

                if (!string.IsNullOrWhiteSpace(txtOutputFolder.Text))
                {
                    dlg.SelectedPath = txtOutputFolder.Text;
                }

                if (dlg.ShowDialog() == WinForms.DialogResult.OK)
                {
                    txtOutputFolder.Text = dlg.SelectedPath;
                }
            }
        }
    }
}