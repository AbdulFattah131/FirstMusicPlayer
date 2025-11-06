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
    public partial class CreatePlaylist : Window
    {
        public CreatePlaylist()
        {
            InitializeComponent();
        }

        //// Create new playlist
        //CreatePlaylistViewModel.Instance.CreateNewPlaylist("My Chill Mix");

        //// Add songs
        //CreatePlaylistViewModel.Instance.AddSongToCurrentPlaylist(selectedSong);

        //// Save
        //CreatePlaylistViewModel.Instance.SaveCurrentPlaylist();

        //// Play existing one
        //var playlist = CreatePlaylistViewModel.Instance.LoadedPlaylists.FirstOrDefault(p => p.Name == "My Chill Mix");
        //        CreatePlaylistViewModel.Instance.PlayPlaylist(playlist);

    }
}
