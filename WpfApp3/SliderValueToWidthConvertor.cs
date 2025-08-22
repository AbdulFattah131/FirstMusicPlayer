using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.UIComponents
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class SliderValueToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Actual width of the slider track
            if (value is double sliderValue && parameter is double totalWidth)
            {
                double max = 100; // or pass in your slider.Max dynamically
                return (sliderValue / max) * totalWidth;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
