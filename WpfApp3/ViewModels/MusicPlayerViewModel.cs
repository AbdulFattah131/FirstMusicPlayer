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
using NAudio.Wave;
using System.Security.Cryptography.X509Certificates;

namespace WpfApp3
{
    public class MusicPlayerCache : INotifyPropertyChanged
    {
        #region Music Player Collections

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
                CurrentIndex = PlaybackQueue.IndexOf(_currentSong);

                if (_currentSong != null)
                {
                    // I am telling my AudioPlayer to load the new song's file
                    Player.Load(_currentSong.FilePath);
                }
                OnPropertyChanged(nameof(CurrentSong));
            }
        }
        public ObservableCollection<Album> Albums { get; set; }  // albums collection
        public int CurrentIndex
        {
            get; private set;
        }
        
        public List<Song> PlaybackQueue = new List<Song>();

        private Album _selectedAlbum; // album navigation
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;

                PlaybackQueue.Clear();
                foreach (Song song in Songs)
                {
                    if (song.Album == _selectedAlbum)
                        PlaybackQueue.Add(song);
                }

                CurrentIndex = 0;
                CurrentSong = PlaybackQueue[CurrentIndex];
                //queue.Add songs of this album from Songs
                OnPropertyChanged(nameof(SelectedAlbum));
            }
        }

        #endregion

        #region Playback Functionality

        // Audio Player 

        private AudioPlayer _player;
        public AudioPlayer Player
        {
            get
            {
                if (_player is null)
                    _player = new();

                return _player;
            }
        }

        // Methods

        public void PlayPause()
        {
            if (CurrentSong == null)
                return;

            if (!Player.IsPlaying)
                Player.Load(CurrentSong.FilePath);
            
            Player.TogglePlayPause();
        }
        
        public void Next()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            Player.Stop();

            CurrentIndex++;

            if (CurrentIndex == PlaybackQueue.Count)
                CurrentIndex = 0;

            CurrentSong = PlaybackQueue[CurrentIndex];
            PlayPause();
        }

        public void Previous()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            Player.Stop();

            CurrentIndex--;

            if (CurrentIndex < 0)
                CurrentIndex = PlaybackQueue.Count - 1;

            CurrentSong = PlaybackQueue[CurrentIndex];
            PlayPause();
        }
        public void Shuffle()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            Player.Stop();

            Random random = new Random();
            int randomIndex = random.Next(PlaybackQueue.Count);

            CurrentIndex = randomIndex;
            CurrentSong = PlaybackQueue[CurrentIndex];
            PlayPause();
        }

        public void Repeat()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            Player.Stop();

            CurrentSong = PlaybackQueue[CurrentIndex];
            PlayPause();
        }

        #endregion

        public MusicPlayerCache()
        {
            var songFilePaths = FileScanner.Instance.ScanSongs(); // song file paths

            Songs = TagReader.Instance.ReadSongsFromFilePaths(songFilePaths); // song objects

            Albums = new ObservableCollection<Album>(TagReader.Instance.GetAlbums()); // albums

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

