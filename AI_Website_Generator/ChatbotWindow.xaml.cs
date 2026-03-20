using System;
using System.Reflection;
using System.Windows;

namespace Orak.WebPro.Admin
{
    public partial class ChatbotWindow : Window
    {
        public ChatbotWindow()
        {
            InitializeComponent();

            try
            {
                dynamic activeX = ChatbotWeb.GetType().InvokeMember(
                    "ActiveXInstance",
                    BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    ChatbotWeb,
                    new object[] { });

                if (activeX != null)
                {
                    activeX.Silent = true;
                }

                ChatbotWeb.Navigate("http://localhost:8000/ChatBotLink.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на чатбота:\n" + ex.Message);
            }
        }
    }
}