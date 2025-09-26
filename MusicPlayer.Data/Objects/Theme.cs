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

        public ThemeColor WindowTextForeground
        {
            get;
            set;
        }

        public ThemeColor WindowPrimary
        {
            get;
            set;
        }
        public ThemeColor WindowPrimaryAnchor
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

        public ThemeColor ListBoxItemHover
        {
            get;
            set;
        }

        public ThemeColor ListBoxItemForeground
        {
            get;
            set;
        }
        public ThemeColor AlternateTextForeground
        {
            get;
            set;
        }
        public ThemeColor TitleBarBackground
        {
            get;
            set;
        }

        public ThemeColor WindowPrimaryLight
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
        public ThemeColor PlayerControlBackground
        {
            get;
            set;
        }
        public ThemeColor ComplementGray
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
