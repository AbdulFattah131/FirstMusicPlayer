using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicPlayer.Data.Objects;

namespace WpfApp3
{
    public class MusicPlayerCache : INotifyPropertyChanged
    {
        private ObservableCollection<Song> _OCSongs;
        public ObservableCollection<Song> Songs
        {
            get => _OCSongs;
            set => _OCSongs = value;
        }

        private Song _currentSong;
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));

            }
        }

        public MusicPlayerCache()
        {
            var song1 = new Song
            {
                Index = 1,
                Title = "Beat It",
                Artist = "Michael Jackson",
                Liked = "♡",
                Duration = 180,
            };
            var song2 = new Song
            {
                Index = 2,
                Title = "Sweet Home Alabama",
                Artist = "Lynyrd Skynyrd",
                Liked = "♡",
                Duration = 230,
            };

            var song3 = new Song
            {
                Index = 3,
                Title = "Better Off Alone",
                Artist = "Alice Deejay",
                Liked = "♡",
                Duration = 156,
            };

            var song4 = new Song
            {
                Index = 4,
                Title = "Immigrant Song",
                Artist = "Led Zeppelin",
                Liked = "♡",
                Duration = 220,
            };

            var song5 = new Song
            {
                Index = 5,
                Title = "Any Way You Want It",
                Artist = "Journey",
                Liked = "♡",
                Duration = 200,
            };

            Songs = new ObservableCollection<Song>();
            Songs.Add(song1);
            Songs.Add(song2);
            Songs.Add(song3);
            Songs.Add(song4);
            Songs.Add(song5);
        }
          
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

