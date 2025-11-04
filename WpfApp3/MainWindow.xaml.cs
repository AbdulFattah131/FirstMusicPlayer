using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
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

            #region Filters for Views

            // Your Library 

            ((CollectionViewSource)FindResource("PopTagAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Michael Jackson" || album.Artist == "Tones and I";
            };

            ((CollectionViewSource)FindResource("FunkTagAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Calvin Harris" || album.Artist == "Mark Ronson";
            };

            ((CollectionViewSource)FindResource("RockTagAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "AC, DC" || album.Artist == "Pink Floyd" || album.Artist == "Led Zeppelin";
            };

            ((CollectionViewSource)FindResource("RnB/SoulTagAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Adele";
            };

            // Explore

            ((CollectionViewSource)FindResource("ContemporarySoulAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Adele";
            };

            ((CollectionViewSource)FindResource("ModernFunkHitsAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Mark Ronson" || album.Artist == "Calvin Harris";
            };

            ((CollectionViewSource)FindResource("HeavyRockAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "AC, DC";
            };

            ((CollectionViewSource)FindResource("Proto-MetalClassicsAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Pink Floyd";
            };

            ((CollectionViewSource)FindResource("ProgressiveRockClassicsAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Led Zeppelin";
            };

            ((CollectionViewSource)FindResource("ModernAlternative/IndieAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Tones and I";
            };

            ((CollectionViewSource)FindResource("Pop-CultureClassicsAlbums")).Filter += (s, e) =>
            {
                var album = (Album)e.Item;
                e.Accepted = album.Artist == "Michael Jackson";
            };

            #endregion

            m_vm = MainWindowViewModel.Instance;
            this.DataContext = m_vm;

            m_vm.Settings = SettingsReader.Instance.ReadFromFile("./Settings.xml");

            LoadPersistence();

            foreach (Theme objTheme in ThemeReader.Instance.GetThemes())
            {
                if(m_vm.Settings.CurrentThemeName == objTheme.Name)
                {
                    m_vm.CurrentTheme = objTheme;
                    break;
                }
            }

            var savedThemes = ThemeReader.Instance.GetThemes();
            bool isCustom = savedThemes.Any(t => t.Name == m_vm.CurrentTheme?.Name);

            if (isCustom)
            {
                tbSwitchTheme.Visibility = Visibility.Hidden;
            }
            else if (m_vm.CurrentTheme == m_vm.DefaultTheme || m_vm.CurrentTheme == m_vm.DefaultTheme2)
            {
                tbSwitchTheme.Visibility = Visibility.Visible;
            }

            btnToggleAlbums.IsChecked = true;
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

        private void borderWindowMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                this.DragMove();
        }

        private void grdClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void grdClose_MouseEnter(object sender, MouseEventArgs e)
        {
            grdClose.Background = (m_vm.CurrentTheme.WindowPrimary.Brush);
        }

        private void grdClose_MouseLeave(object sender, MouseEventArgs e)
        {
            grdClose.Background = Brushes.Transparent;
        }

        private void grdMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.DarkGray;
        }

        private void grdMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.Transparent;
        }

        private void grdMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void grdResize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdResize.Background = Brushes.DarkGray;
        }

        private void grdResize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdResize.Background = null;
        }

        private void grdResize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void Window_Closed(object sender, EventArgs e)
        {
            m_vm.Settings.CurrentThemeName = m_vm.CurrentTheme.Name;
            m_vm.Settings.LastWindowCoordinates = new Point(this.Left, this.Top);
            m_vm.Settings.LastWindowDimensions = new Point(this.Width, this.Height);
            m_vm.Settings.LastKnownVolume = m_vm.MusicPlayerCache.Player.SystemVolume;

            SettingsWriter.Instance.WriteToFile(m_vm.Settings);
        }

        private void grdSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            grdSettings.Background = Brushes.DarkGray;
        }

        private void grdSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            grdSettings.Background = null;
        }

        private void grdSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var window = new ThemeDesigner();
            window.Show();
        }
        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            var m_vm = (MainWindowViewModel)this.DataContext;
            m_vm.SwitchTheme();
        }

        private void HoverZone_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var clickedToggle = sender as ToggleButton;
            foreach (var child in ToggleGroup.Children)
            {
                if (child is Border border)
                {
                    if(border.Child is ToggleButton toggle)
                    {
                        if(toggle != clickedToggle)
                            toggle.IsChecked = false;
                    }
                }
            }
        }

        private void tbPlayPause_Click(object sender, RoutedEventArgs e)
        {
            m_vm.MusicPlayerCache.PlayPause();
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

        #region Playback Controls
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

        private void sdrMusicPlayerSeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void lbAllSongsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

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

        private void grdRightPanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            // Move the content
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void LibraryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox sourceListBox)
                return;

            // Get the newly selected album
            if (sourceListBox.SelectedItem is Album album)
            {
                m_vm.MusicPlayerCache.SelectedAlbum = album;

                // Deselect all other listboxes safely
                var allListBoxes = new[] { lbGenreList1, lbGenreList2, lbGenreList3, lbGenreList4 };
                foreach (var lb in allListBoxes)
                {
                    if (lb != sourceListBox && lb.SelectedItem != null)
                    {
                        lb.SelectedItem = null;
                    }
                }

                // Force update the visuals
                var lbi = (ListBoxItem)sourceListBox.ItemContainerGenerator.ContainerFromItem(album);
                if (lbi != null)
                    lbi.IsSelected = true;
            }
        }
    }
}

