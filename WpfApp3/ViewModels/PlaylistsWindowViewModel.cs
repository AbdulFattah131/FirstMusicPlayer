using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;
using MusicPlayer.UIComponents.ViewModels;
using WpfApp3;

namespace MusicPlayer.UIComponents.ViewModels
{
    public class PlaylistsWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel _mainWindowVM;

        private static PlaylistsWindowViewModel _instance;
        public static PlaylistsWindowViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlaylistsWindowViewModel();
                return _instance;
            }
        }
        public bool IsCreatingPlaylist { get; set; }
        public PlaylistsWindowViewModel(MainWindowViewModel mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            LoadSavedPlaylists();
        }

        public PlaylistsWindowViewModel()
        {
            LoadSavedPlaylists();
        }

        private Playlist _createPlaylist;
        public Playlist CreatePlaylist
        {
            get { return _createPlaylist; }
            set
            {
                _createPlaylist = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CreatePlaylist)));
            }
        }

        public List<Playlist> LoadedPlaylists
        {
            get 
            { 
                return PlaylistReader.Instance.GetPlaylists();
            }
           
        }

        public void CreateNewPlaylist(string name)
        {
            CreatePlaylist = new Playlist() { Name = name, PlaylistSongs = new List<Song>() };
        }

        public void LoadSavedPlaylists()
        {
            var loaded = PlaylistReader.Instance.GetPlaylists();
            //LoadedPlaylists = new ObservableCollection<Playlist>(loaded);
        }

        public void PlayPlaylist(Playlist playlist)
        {
            if (playlist == null || playlist.PlaylistSongs.Count == 0)
                return;

            _mainWindowVM = MainWindowViewModel.Instance;
            _mainWindowVM.MusicPlayerCache.PlayFromPlaylist();
        }

        public void AddSongToCurrentPlaylist(Song song)
        {
            if (CreatePlaylist == null)
                return;

            CreatePlaylist.PlaylistSongs.Add(song);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CreatePlaylist)));
        }

        public void RemoveSongFromCurrentPlaylist(Song song)
        {
            if (CreatePlaylist == null)
                return;

            CreatePlaylist.PlaylistSongs.Remove(song);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CreatePlaylist)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
