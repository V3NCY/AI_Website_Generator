using System.Windows;

namespace AI_Website_Generator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize team data
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
        }
    }
}
