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

                Name = "Lavender",
                WindowTextForeground = new ThemeColor(30, 30, 30),
                WindowPrimaryAnchor = new ThemeColor(142, 125, 183),
                AlternateTextForeground = new ThemeColor(30, 30, 30),
                WindowPrimary = new ThemeColor(197, 178, 239),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(219, 218, 234),
                ComplementGray = new ThemeColor(73, 68, 84),
                WindowPrimaryLight = new ThemeColor(214, 201, 243),
                CurrentSongTitleForeground = new ThemeColor(Brushes.Black),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                PlayerControlBackground = new ThemeColor(231, 234, 240),
                TitleBarBackground = new ThemeColor(46, 44, 54)

            };

            solidTheme2 = new Theme()
            {

                Name = "High Contrast - Lavender",
                WindowTextForeground = new ThemeColor(Brushes.Black),
                WindowPrimaryAnchor = new ThemeColor(142, 125, 183),
                AlternateTextForeground = new ThemeColor(30, 30, 30),
                WindowPrimary = new ThemeColor(205, 166, 255),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(209, 205, 238),
                ComplementGray = new ThemeColor(169, 168, 204),
                WindowPrimaryLight = new ThemeColor(203, 186, 241),
                CurrentSongTitleForeground = new ThemeColor(Brushes.Black),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Black),
                PlayerControlBackground = new ThemeColor(237, 235, 250),
                TitleBarBackground = new ThemeColor(26, 26, 26)

            };

            CurrentTheme = solidTheme;
            ThemeWriter.Instance.WriteToFile(CurrentTheme);

            TagReader.Instance.ReadSongsFromFilePaths(FileScanner.Instance.ScanSongs());

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