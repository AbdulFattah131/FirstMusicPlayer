using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicPlayer.Data.Objects;

namespace WpfApp3
{
    public class MusicPlayerCache : INotifyPropertyChanged
    {
        private List<Song> _lstSongs;
        public List<Song> Songs
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
                OnPropertyChanged(nameof(CurrentSong));
            }
        }
        public MusicPlayerCache()
        {
            var song1 = new Song
            {
                Title = "Beat It",
                Artist = "Michael Jackson",
                AlbumArt = "Images/Beat_It.jpg"
            };
            var song2 = new Song
            {
                Title = "Sweet Home Alabama",
                Artist = "Lynyrd Skynyrd",
                AlbumArt = "Images/Skynyrd-Sweet-Home-Alabama.jpg"
            };

            var song3 = new Song
            {
                Title = "Better Off Alone",
                Artist = "Alice Deejay",
                AlbumArt = "Images/Image-ALice_Deejay_Better_Off_Alone.jpg"
            };

            var song4 = new Song
            {
                Title = "Immigrant Song",
                Artist = "Led Zeppelin",
                AlbumArt = "Images/images.jpg"
            };

            var song5 = new Song
            {
                Title = "Any Way You Want It",
                Artist = "Journey",
                AlbumArt = "Images/Journey_-_Any_Way_You_Want_It_single_cover.jpg"
            };

            Songs = new List<Song>();
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

