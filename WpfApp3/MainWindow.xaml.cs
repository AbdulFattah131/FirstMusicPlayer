using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayer.Data.Objects;
using MusicPlayer.UIComponents;
using MusicPlayer.UIComponents.Constants;
using MusicPlayer.UIComponents.ViewModels;
using MusicPlayer.Utility;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ThemeDesignerViewModel themeDesignerVM;

        public MainWindowViewModel m_vm;

        public MainWindow()
        {
            InitializeComponent();

            InitializeViewModel();

            InitializeSettings();

            LoadPersistence();

            LoadThemes();

            InitializeUIState();
        }

        private void LoadPersistence()
        {
            this.Left = m_vm.Settings.LastWindowCoordinates.X;
            this.Top = m_vm.Settings.LastWindowCoordinates.Y;
            this.Width = m_vm.Settings.LastWindowDimensions.X;
            this.Height = m_vm.Settings.LastWindowDimensions.Y;

            float? fLastKnownVolume = m_vm.Settings.LastKnownVolume;

            if (fLastKnownVolume == null)
                m_vm.MusicPlayerCache.Player.InitializeVolume();
            else
                m_vm.MusicPlayerCache.Player.SystemVolume = (float)m_vm.Settings.LastKnownVolume;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var theme = m_vm.CurrentTheme;
            bool isDefault = theme?.Name is "Lavender" or "Lavender Dark";

            tbSwitchTheme.Visibility = isDefault ? Visibility.Visible : Visibility.Hidden;
            tbSwitchTheme.IsEnabled = isDefault;
            tbSwitchTheme.IsChecked = theme?.Name == "Lavender Dark";
        }

        private void InitializeViewModel()
        {             
            //Initialize ViewModel
            m_vm = MainWindowViewModel.Instance;
            this.DataContext = m_vm;
        }

        private void InitializeSettings()
        {
            //Initialize settings
            m_vm.Settings = SettingsReader.Instance.ReadFromFile("./Settings.xml");
        }

        private void LoadThemes()
        { 
            //Load Themes
            foreach (Theme objTheme in ThemeReader.Instance.GetThemes())
            {
                if (m_vm.Settings.CurrentThemeName == objTheme.Name)
                {
                    m_vm.CurrentTheme = objTheme;
                    break;
                }
            }
        }

        private void InitializeUIState()
        { 
            //Initialize UI State
            btnToggleAlbums.IsChecked = true;
        }

        #region Title Bar

        #region Theme designer Button
        private void bdrThemeDesigner_MouseEnter(object sender, MouseEventArgs e)
        {
            grdThemeDesigner.Background = Brushes.DarkGray;
        }

        private void bdrThemeDesigner_MouseLeave(object sender, MouseEventArgs e)
        {
            grdThemeDesigner.Background = null;
        }

        private ThemeDesigner _themeDesigner;
        private void bdrThemeDesigner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_themeDesigner == null || !_themeDesigner.IsLoaded)
            {
                _themeDesigner = new ThemeDesigner();
                _themeDesigner.Show();
            }
            else
            {
                _themeDesigner.Activate();
            }
        }

        #endregion

        private void bdrSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            grdSettings.Background = Brushes.DarkGray;

        }

        private void bdrSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            grdSettings.Background = null;
        }

        private void bdrSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        #region Close Button
        private void bdrClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bdrClose_MouseEnter(object sender, MouseEventArgs e)
        {
            bdrClose.Background = (m_vm.CurrentTheme.WindowPrimary.Brush);
        }

        private void bdrClose_MouseLeave(object sender, MouseEventArgs e)
        {
            bdrClose.Background = Brushes.Transparent;
        }

        #endregion

        #region Minimize Button
        private void bdrMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.DarkGray;
        }

        private void bdrMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.Transparent;
        }

        private void bdrMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Resize Button
        private void bdrResize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdResize.Background = Brushes.DarkGray;
        }

        private void bdrResize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdResize.Background = null;
        }

        private void bdrResize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                tbResize.Text = "🗖";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                tbResize.Text = "🗗";
            }
        }

        #endregion

        #region On Window Close
        private void Window_Closed(object sender, EventArgs e)
        {
            m_vm.Settings.CurrentThemeName = m_vm.CurrentTheme.Name;
            m_vm.Settings.LastWindowCoordinates = new Point(this.Left, this.Top);
            m_vm.Settings.LastWindowDimensions = new Point(this.Width, this.Height);
            m_vm.Settings.LastKnownVolume = m_vm.MusicPlayerCache.Player.SystemVolume;

            SettingsWriter.Instance.WriteToFile(m_vm.Settings);
        }

        #endregion

        #region Double Click
        private void bdrTitleBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
            }
        }
        #endregion

        #region Move Window (through title bar)
        private void borderWindowMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                this.DragMove();
        }

        #endregion

        #endregion

        #region Switch Button
        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            var m_vm = (MainWindowViewModel)this.DataContext;
            m_vm.SwitchTheme();
        }
        private void UpdateThemeSwitchVisibility()
        {
        }

        #endregion

        #region Playback Controls
        private void tbPlayPause_Click(object sender, RoutedEventArgs e)
        {
            m_vm.MusicPlayerCache.PlayPause();
        }
        private void tbRepeat_Click(object sender, RoutedEventArgs e)
        {
           m_vm.MusicPlayerCache.Repeat();
        }

        private void tbPrevious_Click(object sender, RoutedEventArgs e)
        {
            m_vm.MusicPlayerCache.Previous();
        }

        private void tbNext_Click(object sender, RoutedEventArgs e)
        {
            m_vm.MusicPlayerCache.Next();
        }

        private void tbShuffle_Click(object sender, RoutedEventArgs e)
        {
            m_vm.MusicPlayerCache.Shuffle();
        }

        #endregion

        #region MusicPlayerSeekBar
        private void sdrMusicPlayerSeekBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not MainWindowViewModel m_vm)
                return;

            var slider = sender as Slider;
            if (slider == null) return;

            m_vm.MusicPlayerCache.IsUserDragging = true;

            // Check what was clicked
            if (e.OriginalSource is FrameworkElement fe && fe.TemplatedParent is Thumb)
                return; // allow normal thumb dragging, do NOT handle

            var track = slider.Template.FindName("PART_Track", slider) as System.Windows.Controls.Primitives.Track;
            if (track == null) return;

            Point clickPoint = e.GetPosition(track);
            double ratio = clickPoint.X / track.ActualWidth;
            double newValue = ratio * (slider.Maximum - slider.Minimum) + slider.Minimum;

            slider.Value = newValue;
            double normalized = newValue / slider.Maximum;

            m_vm.MusicPlayerCache.Player.Seek(newValue);

            e.Handled = true; // only handle non-thumb clicks

        }

        private void sdrMusicPlayerSeekBar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel m_vm)
            {
                m_vm.MusicPlayerCache.IsUserDragging = false;
                m_vm.MusicPlayerCache.CurrentPosition = (int)((Slider)sender).Value;
            }
        }

        #endregion

        private new void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            // Move the content
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var clickedToggle = sender as ToggleButton;
            foreach (var child in ToggleGroup.Children)
            {
                if (child is Border border)
                {
                    if (border.Child is ToggleButton toggle)
                    {
                        if (toggle != clickedToggle) 
                        {
                            toggle.IsChecked = false;
                        }
                        else
                        {
                            if (toggle.Name == "btnToggleAlbums")
                            {
                                lbAllSongsList.SelectionChanged -= AllSongsListBox_SelectionChanged;

                                lbAlbumContents.SelectionChanged -= AlbumContentsListBox_SelectionChanged;
                                lbAlbumContents.SelectionChanged += AlbumContentsListBox_SelectionChanged;
                            }

                            if (toggle.Name == "btnToggleSongs")
                            {
                                lbAlbumContents.SelectionChanged -= AlbumContentsListBox_SelectionChanged;

                                lbAllSongsList.SelectionChanged -= AllSongsListBox_SelectionChanged;
                                lbAllSongsList.SelectionChanged += AllSongsListBox_SelectionChanged;
                            }

                            if (toggle.Name == "btnPlaylistSongs")
                            {
                                lbAlbumContents.SelectionChanged -= AlbumContentsListBox_SelectionChanged;
                                lbAllSongsList.SelectionChanged -= AllSongsListBox_SelectionChanged;

                                //lbPlaylistList.SelectionChanged -= lbPlaylistList_SelectionChanaged;
                                //lbPlaylistList.SelectionChanged += lbPlaylistList_SelectionChanaged;
                            }
                        }
                    }
                }
            }
        }

        private void PlaybackToggle_Checked(object sender, RoutedEventArgs e)
        {
            bool? bValue = (sender as ToggleButton).IsChecked;

            if (bValue == null)
            {
                m_vm.MusicPlayerCache.IsPlaying = false;
                return;
            }

            m_vm.MusicPlayerCache.IsPlaying = (bool)bValue;
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender == null)
                return;

            if (sender is ToggleButton btn)
            {
                btn.IsChecked = true;
            }
        }
        #region New Playlist Button

        private PlaylistsWindow _playlistsWindow;
        private void NewPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (_playlistsWindow == null || !_playlistsWindow.IsLoaded)
            {
                _playlistsWindow = new PlaylistsWindow();
                _playlistsWindow.Owner = this;
                _playlistsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _playlistsWindow.Show();
            }
            else
            {
                _playlistsWindow.Activate();
            }
        }

        #endregion

        #region unused code
        //private void LibraryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (sender is not ListBox sourceListBox)
        //        return;

        //    if (sourceListBox.SelectedItem is Album album)
        //    {
        //        m_vm.MusicPlayerCache.SelectedAlbum = album;

        //        var allListBoxes = new[] { lbGenreList1, lbGenreList2, lbGenreList3, lbGenreList4 };
        //        foreach (var lb in allListBoxes)
        //        {
        //            if (lb != sourceListBox && lb.SelectedItem != null)
        //            {
        //                lb.SelectedItem = null;
        //            }
        //        }

        //        var lbi = (ListBoxItem)sourceListBox.ItemContainerGenerator.ContainerFromItem(album);
        //        if (lbi != null)
        //            lbi.IsSelected = true;
        //    }
        //}


        #endregion

        #region unused code
        //#region Filters for Views

        //// Your Library 

        //((CollectionViewSource)FindResource("PopTagAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Michael Jackson" || album.Artist == "Tones and I";
        //};

        //((CollectionViewSource)FindResource("FunkTagAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Calvin Harris" || album.Artist == "Mark Ronson";
        //};

        //((CollectionViewSource)FindResource("RockTagAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "AC, DC" || album.Artist == "Pink Floyd" || album.Artist == "Led Zeppelin";
        //};

        //((CollectionViewSource)FindResource("RnB/SoulTagAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Adele";
        //};

        //// Explore

        //((CollectionViewSource)FindResource("ContemporarySoulAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Adele";
        //};

        //((CollectionViewSource)FindResource("ModernFunkHitsAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Mark Ronson" || album.Artist == "Calvin Harris";
        //};

        //((CollectionViewSource)FindResource("HeavyRockAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "AC, DC";
        //};

        //((CollectionViewSource)FindResource("Proto-MetalClassicsAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Pink Floyd";
        //};

        //((CollectionViewSource)FindResource("ProgressiveRockClassicsAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Led Zeppelin";
        //};

        //((CollectionViewSource)FindResource("ModernAlternative/IndieAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Tones and I";
        //};

        //((CollectionViewSource)FindResource("Pop-CultureClassicsAlbums")).Filter += (s, e) =>
        //{
        //    var album = (Album)e.Item;
        //    e.Accepted = album.Artist == "Michael Jackson";
        //};

        //#endregion
        #endregion

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListBox)?.ScrollIntoView((sender as ListBox)?.SelectedItem);
        }

        private void AlbumContentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListBox)?.ScrollIntoView((sender as ListBox)?.SelectedItem);
        }

        private void AllSongsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox element = sender as ListBox;
            (sender as ListBox)?.ScrollIntoView((sender as ListBox)?.SelectedItem);
            if (MainWindowViewModel.Instance.MusicPlayerCache.CurrentMode != ENMusicPlayerMode.Songs)
                MainWindowViewModel.Instance.MusicPlayerCache.CurrentMode = ENMusicPlayerMode.Songs;
        }
    }
}

