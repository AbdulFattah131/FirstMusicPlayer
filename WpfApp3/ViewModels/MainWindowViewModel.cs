
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp3
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

        public MusicPlayerCache Cache { get; set; }

        private Theme _currentTheme;
        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                _currentTheme = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }
        public Settings Settings
        {
            get; set;
        }
        private Theme solidTheme;
        private Theme gradientTheme;
        private bool useGradient = false;
        public MainWindowViewModel()
        {
            WindowTitle = "Music Player";
            solidTheme = new Theme()
            {

                Name = "Cyan",
                WindowAccent = new ThemeColor(Brushes.Cyan),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(16, 16, 16),
                ListBoxItemForeground = new ThemeColor(Brushes.White),
                CurrentSongTitleForeground = new ThemeColor(Brushes.White),
                CurrentSongArtistForeground = new ThemeColor(Brushes.Gray),
                MusicControlBackground = new ThemeColor(00, 204, 211),
                TitleBarBackground = new ThemeColor(Brushes.Black)

            };
            gradientTheme = new Theme()
            {
                Name = "Gradient Blue Theme",
                WindowAccent = ThemeColor.CreateGradient(
                    Color.FromRgb(0, 99, 90),
                    Color.FromRgb(0, 80, 60)
                ),
                WindowTitleForeground = new ThemeColor(Brushes.White),
                WindowContentBackground = new ThemeColor(16, 16, 16),
                ListBoxItemForeground = new ThemeColor(Brushes.White),
                CurrentSongTitleForeground = new ThemeColor(Brushes.White),
                CurrentSongArtistForeground = ThemeColor.CreateGradient(
                    Color.FromRgb(90, 60, 99),
                    Color.FromRgb(70, 40, 85)
                    ),
                MusicControlBackground = ThemeColor.CreateGradient(
                    Color.FromRgb(99, 20, 80),
                    Color.FromRgb(85, 30, 65)
                    ),
                TitleBarBackground = ThemeColor.CreateGradient(
                    Color.FromRgb(255, 0, 128),
                    Color.FromRgb(0, 255, 255))
            };

            CurrentTheme = solidTheme;
            ThemeWriter.Instance.WriteToFile(CurrentTheme);

            Cache = new MusicPlayerCache();
            Settings = new Settings();
        }

        public void ToggleFullTheme()
        {
            useGradient = !useGradient;
            CurrentTheme = useGradient ? gradientTheme : solidTheme;
        }


        public void SaveTheme(string path)
        {
            var json = JsonConvert.SerializeObject(CurrentTheme,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(path, json);
        }

        public void LoadTheme(string path)
        {
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            var theme = JsonConvert.DeserializeObject<Theme>(json,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            if (theme != null)
                CurrentTheme = theme;
        }
    }
}