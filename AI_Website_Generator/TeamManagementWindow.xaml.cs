using System.Collections.Generic;
using System.Windows;

namespace AI_Website_Generator
{
    public partial class TeamManagementWindow : Window
    {
        public TeamManagementWindow()
        {
            InitializeComponent();
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            List<TeamMember> teamMembers = new List<TeamMember>
            {
                // Designers(2)
                new TeamMember { Name = "Venceslava Georgieva", Role = "Designer" },

                // Request Pickers (4)
                new TeamMember { Name = "Elvira Shugova", Role = "Request Picker" },
                new TeamMember { Name = "Denka Arabadzhiyska", Role = "Request Picker" },
                new TeamMember { Name = "Borislava Dimova", Role = "Request Picker" },
                new TeamMember { Name = "Victoria Dobreva", Role = "Request Picker" },

                // Tech Team (2)
                new TeamMember { Name = "Stoyan Petkov", Role = "Tech Team" },
                new TeamMember { Name = "Georgi Benev", Role = "Tech Team" },

                // Testers (3)
                new TeamMember { Name = "Katya Kalcheva", Role = "Tester" },
                new TeamMember { Name = "Kremena Kairyakova", Role = "Tester" },

                //Sellers (5)
                new TeamMember { Name = "Yordan Totev", Role = "Seller"},
                new TeamMember { Name = "Hristina Boeva", Role = "Seller"},
                new TeamMember { Name = "Hristina Ilcheva", Role = "Seller"},
                new TeamMember { Name = "Tsvetan Karabov", Role = "Seller"},
                new TeamMember { Name = "Nia Yordanova", Role = "Seller"},
            };

            TeamList.ItemsSource = teamMembers;
        }
    }

    public class TeamMember
    {
        public string Name { get; set; } = "Неизвестно лице";
        public string Role { get; set; } = "Без роля";
    }

}
