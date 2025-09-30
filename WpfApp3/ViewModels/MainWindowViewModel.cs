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
                WindowTextForeground = new ThemeColor(38, 38, 38),
                WindowHeadingForeground = new ThemeColor(57, 59, 64),
                WindowPrimaryAnchor = new ThemeColor(114, 91, 164),
                SearchBoxBackground = new ThemeColor(Brushes.White),
                WindowPrimarySlider = new ThemeColor(142, 125, 183),
                AlternateTextForeground = new ThemeColor(Brushes.Gray),
                WindowPrimary = new ThemeColor(197, 178, 239),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(219, 218, 234),
                ComplementGray = new ThemeColor(101, 97, 107),
                CurrentSongTitleForeground = new ThemeColor(Brushes.Black),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                PlayerControlBackground = new ThemeColor(231, 234, 240),
                TitleBarBackground = new ThemeColor(46, 44, 54),
                ListBoxHover = new ThemeColor(248, 248, 255),
                ListBoxSelected = new ThemeColor(Brushes.White),
                LeftToggleHover = new ThemeColor(239, 241, 245),
                LeftToggleSelected = new ThemeColor(197, 178, 239),
                PlayPauseBackground = new ThemeColor(114, 91, 164)

            };

            solidTheme2 = new Theme()
            {

                Name = "Lavender Dark",
                WindowTextForeground = new ThemeColor(238, 238, 247),
                WindowHeadingForeground = new ThemeColor(227, 227, 242),
                WindowPrimaryAnchor = new ThemeColor(197, 178, 239),
                SearchBoxBackground = new ThemeColor(106, 103, 131),
                WindowPrimarySlider = new ThemeColor(197, 178, 239),
                AlternateTextForeground = new ThemeColor(204, 204, 204),
                WindowPrimary = new ThemeColor(197, 178, 239),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(68, 67, 90),
                ComplementGray = new ThemeColor(237, 235, 250),
                CurrentSongTitleForeground = new ThemeColor(Brushes.Black),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                PlayerControlBackground = new ThemeColor(77, 78, 101),
                TitleBarBackground = new ThemeColor(46, 44, 54),
                ListBoxHover = new ThemeColor(92, 90, 114),
                ListBoxSelected = new ThemeColor(106, 103, 131),
                LeftToggleHover = new ThemeColor(92, 90, 114),
                LeftToggleSelected = new ThemeColor(106, 103, 131),
                PlayPauseBackground = new ThemeColor(197, 178, 239)

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