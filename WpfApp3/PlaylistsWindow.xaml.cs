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

namespace MusicPlayer.UIComponents
{
    /// <summary>
    /// Interaction logic for CreatePlaylist.xaml
    /// </summary>
    public partial class PlaylistsWindow : Window
    {
        public PlaylistsWindow()
        {
        }

        // Create new playlist
        PlaylistsWindowViewModel

        //// Add songs
        //PlaylistViewModel.Instance.AddSongToCurrentPlaylist(selectedSong);

        //// Save
        //PlaylistViewModel.Instance.SaveCurrentPlaylist();

        //// Play existing one
        //var playlist = PlaylistViewModel.Instance.LoadedPlaylists.FirstOrDefault(p => p.Name == "My Chill Mix");
        //PlaylistViewModel.Instance.PlayPlaylist(playlist);

    }
}
