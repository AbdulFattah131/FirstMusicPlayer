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

        private ObservableCollection<Song> _lstSongs; // collection of all songs
        public ObservableCollection<Song> Songs
        {
            get => _lstSongs;
            set => _lstSongs = value;
        }

        private ObservableCollection<Song> _obAllSongs;
        public ObservableCollection<Song> AllSongs
        {
            get => _obAllSongs;
            set => _obAllSongs = value;
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

        private ENMusicPlayerMode _currentMode = ENMusicPlayerMode.Albums;
        public ENMusicPlayerMode CurrentMode
        {
            get => _currentMode;
            set
            {
                _currentMode = value;
                
                switch (CurrentMode)
                {
                    case ENMusicPlayerMode.Songs:
                        PlaybackQueue = new ObservableCollection<Song>(AllSongs);
                        break;
                    case ENMusicPlayerMode.Albums:
                        break;
                }

                OnPropertyChanged(nameof(CurrentMode));
            }
        }
        public bool IsPlaying
        {
            get => Player.IsPlaying;
            set
            {
                Player.TogglePlayPause();
                OnPropertyChanged(nameof(IsPlaying));
            }
        }

        private Album _selectedAlbum; // album navigation
        private Album _previousSelectedAlbum;
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                if (_selectedAlbum != null)
                    _previousSelectedAlbum = _selectedAlbum;

                _selectedAlbum = value;
                Player.Stop();
                
                CurrentSong = null;

                if (_selectedAlbum != null)
                {
                    CurrentMode = ENMusicPlayerMode.Albums;
                    PlaybackQueue = new ObservableCollection<Song>(SelectedAlbum.Songs);
                }

                if (PlaybackQueue.Count > 0)
                {
                    CurrentIndex = 0;
                    CurrentSong = PlaybackQueue[CurrentIndex];
                }
                
                OnPropertyChanged(nameof(SelectedAlbum));
            }
        }

        private Song _currentSong; // currently selected song
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                if (_currentSong == value)
                    return;

                Player.Stop();
                _currentSong = value;
                CurrentPosition = 0;

                if (value == null)
                    return;

                CurrentIndex = PlaybackQueue.IndexOf(_currentSong);

                if (_currentSong != null)
                    Player.Load(_currentSong.FilePath);
                

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

        public void PlayFromPlaylist()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;
            CurrentIndex = 0;
            CurrentSong = PlaybackQueue[CurrentIndex];
        }

        private string _loadedFilePath;
        public void PlayPause()
        {
            if (CurrentSong == null && PlaybackQueue == null || PlaybackQueue.Count == 0 && CurrentIndex < 0 || CurrentIndex >= PlaybackQueue.Count)
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
        }

        public void Shuffle()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            Random random = new Random();

            Song[] _arrTempQueue = PlaybackQueue.ToArray();
            random.Shuffle(_arrTempQueue);
            PlaybackQueue = new ObservableCollection<Song>(_arrTempQueue);
            CurrentIndex = PlaybackQueue.IndexOf(CurrentSong);
        }

        private ENMusicPlayerRepeatMode _repeatMode = ENMusicPlayerRepeatMode.None;
        public ENMusicPlayerRepeatMode RepeatMode
        {
            get => _repeatMode;
            set
            {
                _repeatMode = value;

                switch (value)
                {
                    case ENMusicPlayerRepeatMode.RepeatList:
                        if (CurrentIndex == PlaybackQueue.Count - 1)
                        {
                            CurrentIndex = 0;
                            CurrentSong = PlaybackQueue[CurrentIndex];
                            PlayPause();
                        }
                        break;
                    case ENMusicPlayerRepeatMode.RepeatSong:
                        CurrentSong = PlaybackQueue[CurrentIndex];
                        PlayPause();
                        break;
                    case ENMusicPlayerRepeatMode.None:
                        break;
                }
            }
        }

        public void Repeat()
        {
            if (PlaybackQueue == null || PlaybackQueue.Count == 0)
                return;

            



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

                    if (_isUserDragging)
                    {
                        double progress = _currentPosition / Player.TotalTime.TotalSeconds;
                        Player.Seek(progress);
                    }

                    OnPropertyChanged(nameof(CurrentPosition));
                }
            }
        }
        #endregion

        public MusicPlayerCache()
        {
            var songFilePaths = FileScanner.Instance.ScanSongs();             // song file paths

            AllSongs = TagReader.Instance.ReadSongsFromFilePaths(songFilePaths); // song objects

            Albums = new ObservableCollection<Album>(TagReader.Instance.GetAlbums());  // albums

            PlaybackQueue = new ObservableCollection<Song>();                  // playback queue

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

            CurrentPosition = (int)Player.CurrentTime.TotalSeconds;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

