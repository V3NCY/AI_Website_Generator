using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AI_Website_Generator
{
    public partial class ManageDesignsWindow : Window
    {
        public ManageDesignsWindow()
        {
            InitializeComponent();
        }

        private void TemplateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TemplateList.SelectedItem is ListBoxItem selectedItem && selectedItem.Tag is string filePath)
            {
                try
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

                    if (File.Exists(fullPath))
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = fullPath,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Template not found: " + fullPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening template: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
