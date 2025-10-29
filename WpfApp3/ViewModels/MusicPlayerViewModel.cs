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
using System.Windows.Threading;
using System.Windows.Data;
using MusicPlayer.UIComponents.Constants;
using NAudio.CoreAudioApi;

namespace WpfApp3
{
    public class MusicPlayerCache : INotifyPropertyChanged
    {
        #region Music Player Collections

        private ObservableCollection<Song> _lstSongs; // list of all songs
        public ObservableCollection<Song> Songs
        {
            get => _lstSongs;
            set => _lstSongs = value;
        }

        public ObservableCollection<Album> Albums { get; set; }  // albums collection
        public int CurrentIndex
        {
            get; private set;
        }

        private ObservableCollection<Song> _ocPlaybackQueue;
        public ObservableCollection<Song> PlaybackQueue
        {
            get => _ocPlaybackQueue;
            set
            {
                _ocPlaybackQueue = value;
                OnPropertyChanged(nameof(PlaybackQueue));
            }
        }

        public bool IsPlaying
        {
            get => _player.IsPlaying;
            set
            {
                _player.TogglePlayPause();
                OnPropertyChanged(nameof(IsPlaying));
            }
        }

        private Album _selectedAlbum; // album navigation
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                _player.Stop();

                CurrentSong = null;

                PlaybackQueue = _selectedAlbum.Songs;

                if (PlaybackQueue.Count > 0)
                {
                    CurrentIndex = 0;
                    CurrentSong = PlaybackQueue[CurrentIndex];
                }

                //queue.Add songs of this album from Songs
                OnPropertyChanged(nameof(SelectedAlbum));
            }
        }

        private Song _currentSong; // currently selected song
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                Player.Stop();
                _currentSong = value;
                CurrentPosition = 0;

                if (value == null)
                    return;

                CurrentIndex = PlaybackQueue.IndexOf(_currentSong);

                if (_currentSong != null)
                {
                    // I am telling my AudioPlayer to load the new song's file
                    Player.Load(_currentSong.FilePath);
                }

                _player.TogglePlayPause();
                OnPropertyChanged(nameof(CurrentSong));
                OnPropertyChanged(nameof(IsPlaying));
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

        private AudioPlayer _audioFile;
        public AudioPlayer AudioFile
        {
            get
            {
                if (_audioFile is null)
                    _audioFile = new();

                return _audioFile;
            }
        }

        public AudioPlayer MMDevice;

        private readonly DispatcherTimer _timer;

        // Playback Control Buttons

        private string _loadedFilePath;
        public void PlayPause()
        {
            if (CurrentSong == null)
                return;

            if (_loadedFilePath != CurrentSong.FilePath)
                Player.Load(CurrentSong.FilePath);
                _loadedFilePath = CurrentSong.FilePath;

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


        #region Slider (Music Seek Bar)

        private bool _isUserDragging;
        public bool IsUserDragging
        {
            get => _isUserDragging;
            set
            {
                _isUserDragging = value;
                OnPropertyChanged(nameof(IsUserDragging));
            }
        }

        private int _currentPosition;
        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (_currentPosition != value)
                {
                    _currentPosition = value;

                    if (_isUserDragging && _player != null)
                    {
                        double progress = _currentPosition / _player.TotalTime.TotalSeconds;
                        _player.Seek(progress);
                    }

                    OnPropertyChanged(nameof(CurrentPosition));
                }
            }
        }
        #endregion

        public MusicPlayerCache()
        {
            var songFilePaths = FileScanner.Instance.ScanSongs(); // song file paths

            Songs = TagReader.Instance.ReadSongsFromFilePaths(songFilePaths); // song objects

            Albums = new ObservableCollection<Album>(TagReader.Instance.GetAlbums()); // albums

            PlaybackQueue = new ObservableCollection<Song>();

            #region Timer
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            #endregion

        }

        // Timer Tick
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!IsPlaying)
                return;

            if (_player != null)
                CurrentPosition = (int)_player.CurrentTime.TotalSeconds;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

