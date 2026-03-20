using System;
using System.Windows;
using Orak.WebPro.Admin.user;

namespace Orak.WebPro.Admin
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown;

                if (TeamManagementWindow.TeamMembers.Count == 0)
                {
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Denka Arabadzhiyska", Role = "Request Picker" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Borislava Dimova", Role = "Request Picker" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Victoria Dobreva", Role = "Request Picker" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Georgi Benev", Role = "Tech Team" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Katya Kalcheva", Role = "Tester" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Kremena Kairyakova", Role = "Tester" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Yordan Totev", Role = "Seller" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Hristina Boeva", Role = "Seller" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Hristina Ilcheva", Role = "Seller" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Tsvetan Karabov", Role = "Seller" });
                    TeamManagementWindow.TeamMembers.Add(new TeamMember { Name = "Nia Yordanova", Role = "Seller" });
                }

                var loginWindow = new LoginWindow();
                bool? loginResult = loginWindow.ShowDialog();

                if (loginResult != true)
                {
                    Shutdown();
                    return;
                }

                Window windowToOpen = null;

                switch (loginWindow.LoggedInRole)
                {
                    case "Admin":
                        windowToOpen = new MainWindow();
                        break;

                    case "Support":
                        windowToOpen = new TechnicalSupportWindow();
                        break;

                    case "Team":
                        windowToOpen = new TeamManagementWindow();
                        break;

                    case "Client":
                        windowToOpen = new MainWindow();
                        break;

                    default:
                        MessageBox.Show($"Невалидна роля: {loginWindow.LoggedInRole}");
                        Shutdown();
                        return;
                }

                MainWindow = windowToOpen;
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                windowToOpen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Startup crash");
                Shutdown();
            }
        }
    }
}