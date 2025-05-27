// File: Models/Website.cs
using System.ComponentModel;

namespace AI_Website_Generator.Models
{
    public class Website : INotifyPropertyChanged
    {
        private string name;
        private string status;
        private string errorDetails;
        private string aiInsights;
        private string recommendedAction;
        private int aiConfidence;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string ErrorDetails
        {
            get => errorDetails;
            set { errorDetails = value; OnPropertyChanged(nameof(ErrorDetails)); }
        }

        public string AIInsights
        {
            get => aiInsights;
            set { aiInsights = value; OnPropertyChanged(nameof(AIInsights)); }
        }

        public string RecommendedAction
        {
            get => recommendedAction;
            set { recommendedAction = value; OnPropertyChanged(nameof(RecommendedAction)); }
        }

        public int AIConfidence

        {
            get => aiConfidence;
            set { aiConfidence = value; OnPropertyChanged(nameof(AIConfidence)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
