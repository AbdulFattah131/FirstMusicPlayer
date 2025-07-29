using System.ComponentModel;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MusicPlayer.Data.Objects
{
    public class ThemeColor : INotifyPropertyChanged
    {
        [XmlIgnore]
        public Brush Brush { get; set; }
        public bool IsGradient { get; set; } = false;
        public string Hex
        {
            get
            {
                if (Brush == null)
                    return "#FF000000";

                return Brush.ToString(); //#FF00000
            }

            set
            {
                if (value == null || (value.Length != 9 && value.Length != 7))
                    return;

                Brush = (SolidColorBrush)(new BrushConverter().ConvertFromString(value));
                OnPropertyChanged(new PropertyChangedEventArgs("Brush"));
            }
        }

        public ThemeColor() {
            Brush = Brushes.Black;
        }

        public ThemeColor (Brush brush)
        {
            this.Brush = brush;
            if (brush is SolidColorBrush solid)
            {
                Hex = solid.Color.ToString();
                IsGradient = false;
            }
            else
            {
                Hex = null;
                IsGradient = true;
            }
        }
        public static ThemeColor CreateGradient(Color startColor, Color endColor)
        {
            var gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(new GradientStop(startColor, 0.0));
            gradient.GradientStops.Add(new GradientStop(endColor, 1.0));
            return new ThemeColor(gradient) { IsGradient = true };
        }
        public ThemeColor(byte r, byte g, byte b)
        {
            this.Brush = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
