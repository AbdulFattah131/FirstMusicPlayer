using System.ComponentModel;
using System.Windows;

namespace MusicPlayer.Data.Objects
{
    public class Theme : INotifyPropertyChanged
    {
        public string Name
        {
            get;
            set;
        }

        public ThemeColor WindowAccent
        {
            get;
            set;
        }

        public ThemeColor WindowContentBackground
        {
            get;
            set;
        }
        
        public ThemeColor WindowTitleForeground
        {
            get;
            set;
        }

        public ThemeColor ListBoxItemForeground
        {
            get;
            set;
        }

        public ThemeColor CurrentSongTitleForeground
        {
            get;
            set;
        }
        
        public ThemeColor CurrentSongArtistForeground
        {
            get;
            set;
        }
        public ThemeColor MusicControlBackground
        {
            get;
            set;
        }
        public ThemeColor TitleBarBackground
        {
            get;
            set;
        }

        public Theme ()
        {
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
