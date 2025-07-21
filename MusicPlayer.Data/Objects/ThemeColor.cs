using System.Windows.Media;
using System.Xml.Serialization;

namespace MusicPlayer.Data.Objects
{
    public class ThemeColor
    {
        [XmlIgnore]
        public Brush Brush { get; set; }
        public string Hex
        {
            get
            {
                return Brush.ToString(); //#FF00000
            }

            set
            {
                Brush = (SolidColorBrush)(new BrushConverter().ConvertFromString(value));
            }
        }

        public ThemeColor()
        {
        }

        public ThemeColor (Brush brush)
        {
            this.Brush = brush;
        }

        public ThemeColor(byte r, byte g, byte b)
        {
            this.Brush = new SolidColorBrush(Color.FromRgb(r, g, b));
        }
    }
}
