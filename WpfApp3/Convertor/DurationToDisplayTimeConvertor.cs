using System.Globalization;
using System.Windows.Data;

namespace MusicPlayer.UIComponents.Convertor

{
    public class DurationToDisplayTimeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int)
            {
                return "--:--";
            }

            int nDuration = (int)value;

            if (nDuration is <=0)
            {
                return string.Empty;
            }

            int nMinutes = nDuration / 60;
            int nSeconds = nDuration % 60;

            return $"{nMinutes:00}:{nSeconds:00}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
