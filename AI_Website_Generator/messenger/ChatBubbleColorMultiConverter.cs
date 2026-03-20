using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Orak.WebPro.Admin
{
    public class ChatBubbleColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is bool isMe) || !(values[1] is bool isLatest))
                return Brushes.LightGray;

            if (isLatest)
                return isMe ? Brushes.LightSkyBlue : Brushes.LightGreen;

            return isMe ? Brushes.WhiteSmoke : Brushes.LightGray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
