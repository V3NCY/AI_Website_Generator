using System;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class ChatbotWindow : Window
    {
        public ChatbotWindow()
        {
            InitializeComponent();

            string chatbotUrl = "http://localhost:8000/ChatBotLink.html";

            try
            {
                ChatbotWeb.Navigate(chatbotUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на чатбота: " + ex.Message);
            }
        }
    }
}