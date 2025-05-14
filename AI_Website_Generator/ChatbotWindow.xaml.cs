using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class ChatbotWindow : Window
    {
        public ChatbotWindow()
        {
            InitializeComponent();

            // Get the full path to the HTML file
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", "ChatBotLink.html");

            if (File.Exists(templatePath))
            {
                try
                {
                    var uri = new Uri(templatePath).AbsoluteUri; // Converts to file:///C:/... format
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = new Uri(templatePath).AbsoluteUri,
                        UseShellExecute = true
                    });

                    // Optional: don't show window at all if you only need to trigger the link
                    this.Visibility = Visibility.Collapsed;

                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при отваряне на браузър: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Не може да бъде намерен файлът: " + templatePath, "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
