
using System.Windows.Media;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;

namespace WpfApp3
{
    public class MainWindowViewModel
    {
        public string WindowTitle
        {
            get;
            set;
        }

        public MusicPlayerCache Cache { get; set; }

        public Theme CurrentTheme
        {
            get; set;
        }
        public Settings Settings
        {
            get; set;
        } 

        public MainWindowViewModel ()
        {
            WindowTitle = "Music Player";
            CurrentTheme = new Theme()
            {
                Name = "Cyan",
                WindowAccent = new ThemeColor (Brushes.Cyan),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(16, 16, 16),
                ListBoxItemForeground = new ThemeColor(Brushes.White),
                CurrentSongTitleForeground = new ThemeColor(Brushes.White),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                MusicControlBackground = new ThemeColor(00, 204, 211),
                TitleBarBackground = new ThemeColor(Brushes.Black)

            };

            ThemeWriter.Instance.WriteToFile(CurrentTheme);

            Cache = new MusicPlayerCache();
            Settings = new Settings();
        }


    }
}
