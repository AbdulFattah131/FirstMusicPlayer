using System.ComponentModel;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;
using WpfApp3;

namespace MusicPlayer.UIComponents.ViewModels
{
    public class ThemeDesignerViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel _mainWindowVM;
        public ThemeDesignerViewModel(MainWindowViewModel Instance)
        {

            CustomTheme = new Theme()
            {
                WindowAccent = new ThemeColor(),
                WindowContentBackground = new ThemeColor(),
                WindowTitleForeground = new ThemeColor(),
                ListBoxItemForeground = new ThemeColor(),
                CurrentSongTitleForeground = new ThemeColor(),
                CurrentSongArtistForeground = new ThemeColor(),
                MusicControlBackground = new ThemeColor(),
                TitleBarBackground = new ThemeColor(),
            };
        }

        public void ApplyTheme(Theme theme)
        {
            MainWindowViewModel.Instance.CurrentTheme = CustomTheme;
        }

        private Theme m_customTheme;

        public Theme CustomTheme
        {
            get { return m_customTheme; }
            set
            {
                m_customTheme = value;
                OnPropertyChanged(new PropertyChangedEventArgs(""));
            }
        }  

        public List<Theme> LoadedThemes
        {
            get
            {
                return ThemeReader.Instance.GetThemes();
            }
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
