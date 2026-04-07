using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Orak.WebPro.Admin.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value != null ? value.ToString() ?? string.Empty : string.Empty;

            switch (status)
            {
                case "OK":
                case "Active":
                case "Работи":
                    return Brushes.LimeGreen;

                case "Проблем":
                case "Warning":
                    return Brushes.Goldenrod;

                case "Error":
                case "Грешка":
                    return Brushes.IndianRed;

                default:
                    return Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}