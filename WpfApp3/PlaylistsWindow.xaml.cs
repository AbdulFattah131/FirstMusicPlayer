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
using MusicPlayer.UIComponents.ViewModels;
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
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void borderWindowMove_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void borderWindowMove_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void bdrMinimize_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        // Create new playlist
        //PlaylistsWindowViewModel

        //// Add songs
        //PlaylistViewModel.Instance.AddSongToCurrentPlaylist(selectedSong);

        //// Save
        //PlaylistViewModel.Instance.SaveCurrentPlaylist();

        //// Play existing one
        //var playlist = PlaylistViewModel.Instance.LoadedPlaylists.FirstOrDefault(p => p.Name == "My Chill Mix");
        //PlaylistViewModel.Instance.PlayPlaylist(playlist);

    }
}
