using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MusicPlayer.Data.Objects;
using MusicPlayer.UIComponents.ViewModels;
using MusicPlayer.Utility;
using WpfApp3;

namespace MusicPlayer.UIComponents
{
    /// <summary>
    /// Interaction logic for CreatePlaylist.xaml
    /// </summary>
    public partial class PlaylistsWindow : Window
    {
        public MainWindowViewModel m_vm;
        public PlaylistsWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void PlaylistWindow_Closed(object sender, EventArgs e)
        {
            
        }

        private void PlaylistWindow_Loaded(object sender, EventArgs e)
        {

        }
        private void btnSavePlaylist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistWriter.Instance.WriteToFile(PlaylistsWindowViewModel.Instance.CreatePlaylist.PlaylistSongs);
            MessageBox.Show("Theme saved!");

        }

        public bool IsCreatingPlaylist { get; set; } = false;

        public List<Song> NewPlaylistSongs { get; set; } = new List<Song>();

        private void btnCreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            IsCreatingPlaylist = true;
            plSongsListBox.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
