using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AI_Website_Generator
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value?.ToString()?.ToLower() ?? "";

            return status switch
            {
                "получена заявка" => new SolidColorBrush(Colors.SteelBlue),
                "в процес" => new SolidColorBrush(Colors.Orange),
                "за дизайнер" => new SolidColorBrush(Colors.Orchid),
                "за начално тестване" => new SolidColorBrush(Colors.MediumPurple),
                "за финално тестване" => new SolidColorBrush(Colors.MediumVioletRed),
                "за технически екип" => new SolidColorBrush(Colors.Teal),
                "проблем" => new SolidColorBrush(Colors.Red),
                "онлайн" => new SolidColorBrush(Colors.Green),
                _ => new SolidColorBrush(Colors.Gray),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
