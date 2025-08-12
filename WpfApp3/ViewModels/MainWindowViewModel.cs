
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;
using System.IO;
using System.Xml.Serialization;

namespace WpfApp3
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static MainWindowViewModel _instance;
        public static MainWindowViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainWindowViewModel();
                return _instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string WindowTitle
        {
            get;
            set;
        }

        public MusicPlayerCache MusicPlayerCache { get; set; }

        private Theme _currentTheme;
        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnPropertyChanged(nameof(CurrentTheme));
                }
            }
        }
        public Settings Settings
        {
            get; set;
        }

        private Theme solidTheme;
        private Theme solidTheme2;

        public MainWindowViewModel()
        {
            WindowTitle = "Music Player";
            solidTheme = new Theme()
            {

                Name = "Lavender Gray",
                WindowAccent = new ThemeColor(197, 178, 239),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(219, 218, 234),
                ListBoxItemForeground = new ThemeColor(Brushes.Black),
                CurrentSongTitleForeground = new ThemeColor(Brushes.Black),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                MusicControlBackground = new ThemeColor(231, 234, 240),
                TitleBarBackground = new ThemeColor(Brushes.Black)

            };

            solidTheme2 = new Theme()
            {

                Name = "Red",
                WindowAccent = new ThemeColor(Brushes.Red),
                WindowTitleForeground = new ThemeColor(255, 235, 235),
                WindowContentBackground = new ThemeColor(255, 255, 255),
                ListBoxItemForeground = new ThemeColor(255, 235, 235),
                CurrentSongTitleForeground = new ThemeColor(255, 235, 235),
                CurrentSongArtistForeground = new ThemeColor(255, 185, 185),
                MusicControlBackground = new ThemeColor(228, 57, 57),
                TitleBarBackground = new ThemeColor(255, 162, 162)

            };

            CurrentTheme = solidTheme;
            ThemeWriter.Instance.WriteToFile(CurrentTheme);

            MusicPlayerCache = new MusicPlayerCache();
            Settings = new Settings();
        }

        public void SwitchTheme()
        {
            CurrentTheme = CurrentTheme == solidTheme ? solidTheme2 : solidTheme;
        }

        public void SaveTheme(string path)
        {
            var serializer = new XmlSerializer(typeof(Theme));
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, CurrentTheme);
            }
        }

        public void LoadTheme(string path)
        {
            if (!File.Exists(path)) return;

            var serializer = new XmlSerializer(typeof(Theme));
            using (var reader = new StreamReader(path))
            {
                var theme = (Theme)serializer.Deserialize(reader);
                if (theme != null)
                    CurrentTheme = theme;
            }
        }
    }
}