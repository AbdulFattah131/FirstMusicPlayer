
using System.Windows;

namespace MusicPlayer.Data.Objects
{
    public class Settings
    {
        public string CurrentThemeName
        {
            get;
            set;
        }
        public Point LastWindowCoordinates 
        { 
            get; 
            set; 
        }

        public Point LastWindowDimensions
        {
            get;
            set;
        }

        public float? LastKnownVolume
        {
            get;
            set;
        }
    }
}
