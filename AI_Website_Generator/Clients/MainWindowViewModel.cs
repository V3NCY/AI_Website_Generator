using Orak.WebPro.Admin.Clients;

namespace Orak.WebPro.Admin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _currentUsername = "Администратор";
        private int _newRequestsCount = 6;
        private int _websitesInBuildCount = 8;
        private int _liveWebsitesCount = 12;
        private int _attentionItemsCount = 3;

        public string CurrentUsername
        {
            get => _currentUsername;
            set { _currentUsername = value; OnPropertyChanged(); }
        }

        public int NewRequestsCount
        {
            get => _newRequestsCount;
            set { _newRequestsCount = value; OnPropertyChanged(); }
        }

        public int WebsitesInBuildCount
        {
            get => _websitesInBuildCount;
            set { _websitesInBuildCount = value; OnPropertyChanged(); }
        }

        public int LiveWebsitesCount
        {
            get => _liveWebsitesCount;
            set { _liveWebsitesCount = value; OnPropertyChanged(); }
        }

        public int AttentionItemsCount
        {
            get => _attentionItemsCount;
            set { _attentionItemsCount = value; OnPropertyChanged(); }
        }
    }
}