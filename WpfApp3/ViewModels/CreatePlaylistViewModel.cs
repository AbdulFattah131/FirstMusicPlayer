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
    public class CreatePlaylistViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel _mainWindowVM;

        private static CreatePlaylistViewModel _instance;
        public static CreatePlaylistViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CreatePlaylistViewModel();
                return _instance;
            }
        }

        public CreatePlaylistViewModel(MainWindowViewModel mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            LoadSavedPlaylists();
        }

        public CreatePlaylistViewModel()
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

        private ObservableCollection<Playlist> _loadedPlaylists = new ObservableCollection<Playlist>();
        public ObservableCollection<Playlist> LoadedPlaylists
        {
            get { return _loadedPlaylists; }
            set
            {
                _loadedPlaylists = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LoadedPlaylists)));
            }
        }

        public void CreateNewPlaylist(string name)
        {
            CreatePlaylist = new Playlist() { Name = name, PlaylistSongs = new ObservableCollection<Song>() };
        }

        public void SaveCurrentPlaylist()
        {
            if (CreatePlaylist == null || string.IsNullOrWhiteSpace(CreatePlaylist.Name))
                return;

            var playlists = PlaylistReader.Instance.GetPlaylists();

            // Replace existing playlist with same name
            var existing = playlists.FirstOrDefault(p => p.Name == CreatePlaylist.Name);
            if (existing != null)
                playlists.Remove(existing);

            playlists.Add(CreatePlaylist);
            PlaylistWriter.Instance.WriteToFile(playlists);

            LoadSavedPlaylists();
        }

        public void LoadSavedPlaylists()
        {
            var loaded = PlaylistReader.Instance.GetPlaylists();
            LoadedPlaylists = new ObservableCollection<Playlist>(loaded);
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
