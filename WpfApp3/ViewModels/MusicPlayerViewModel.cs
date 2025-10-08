using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;
using MusicPlayer.Data.Objects;
using MusicPlayer.UIComponents.ViewModels;
using MusicPlayer.Utility;

namespace WpfApp3
{
    public class MusicPlayerCache : INotifyPropertyChanged
    {
        private ObservableCollection<Song> _lstSongs;
        public ObservableCollection<Song> Songs
        {
            get => _lstSongs;
            set => _lstSongs = value;
        }

        private Song _currentSong;
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;
                if (_currentSong != null)
                {
                    // I am telling my AudioPlayer to load the new song's file
                    _player.Load(_currentSong.FilePath);
                }
                OnPropertyChanged(nameof(CurrentSong));
            }
        }
        public ObservableCollection<Album> Albums { get; set; }

        private Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                OnPropertyChanged(nameof(SelectedAlbum));
            }
        }

        // Audio Player 

        private readonly AudioPlayer _player;

        public RelayCommand PlayPauseCommand;
        public RelayCommand ShuffleCommand;
        public RelayCommand RepeatCommand;
        public RelayCommand PreviousCommand;
        public RelayCommand NextCommand;
        public RelayCommand VolumeCommand;
     
        public MusicPlayerCache()
        {
            var songFilePaths = FileScanner.Instance.ScanSongs(); // song file paths

            var songs = TagReader.Instance.ReadSongsFromFilePaths(songFilePaths); // song objects

            Albums = new ObservableCollection<Album>(TagReader.Instance.GetAlbums()); // albums

            _player = new AudioPlayer(); // playback tool initialization

        }

        public bool Flag { get; set; } = false;
        public string DominantGenre
        {
            get
            {
                var genres = SelectedAlbum?.Genres;
                if (genres == null || genres.Count == 0)
                    return "No Genre";

                // Show all genres joined, or just one depending on a flag
                return Flag
                    ? string.Join(", ", genres)
                    : genres.FirstOrDefault();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

