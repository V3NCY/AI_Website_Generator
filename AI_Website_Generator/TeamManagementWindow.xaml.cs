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
                // Designers (2)
                new TeamMember { Name = "Ivan Petrov", Role = "Designer" },
                new TeamMember { Name = "Maria Georgieva", Role = "Designer" },

                // Request Pickers (4)
                new TeamMember { Name = "Petar Dimitrov", Role = "Request Picker" },
                new TeamMember { Name = "Elena Nikolova", Role = "Request Picker" },
                new TeamMember { Name = "Stefan Ivanov", Role = "Request Picker" },
                new TeamMember { Name = "Nikolay Todorov", Role = "Request Picker" },

                // Tech Team (2)
                new TeamMember { Name = "Dimitar Stoyanov", Role = "Tech Team" },
                new TeamMember { Name = "Borislav Hristov", Role = "Tech Team" },

                // Testers (3)
                new TeamMember { Name = "Katerina Yordanova", Role = "Tester" },
                new TeamMember { Name = "Veselin Petkov", Role = "Tester" },
                new TeamMember { Name = "Lyubomir Kirov", Role = "Tester" }
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
