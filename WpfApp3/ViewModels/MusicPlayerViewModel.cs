using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;
using MusicPlayer.Data.Objects;
using MusicPlayer.UIComponents.ViewModels;
using MusicPlayer.Utility;
using System.Windows;

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
        public ObservableCollection<Album> Albums { get; set; }  // albums collection

        //public ObservableCollection<Album> PopTagAlbums;

        private Album _selectedAlbum; // album navigation
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

     
        public MusicPlayerCache()
        {
            var songFilePaths = FileScanner.Instance.ScanSongs(); // song file paths

            var songs = TagReader.Instance.ReadSongsFromFilePaths(songFilePaths); // song objects

            Albums = new ObservableCollection<Album>(TagReader.Instance.GetAlbums()); // albums

            _player = new AudioPlayer(); // playback tool initialization

            //PopTagAlbums = new ObservableCollection<Album>(Albums.Where(a => a.Artist == "Michael Jackson" || a.Artist == "Tones and I"));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

