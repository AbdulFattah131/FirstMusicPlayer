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

        private List<Song> _similarSongs;
        public List<Song> SimilarSongs
        {
            get => _similarSongs;
            set
            {
                _similarSongs = value;
                OnPropertyChanged(nameof(SimilarSongs));

                //LoadSimilarSongs(_currentSong);
            }
        }
        //private void LoadSimilarSongs(Song selectedSong)
        //{
        //    SimilarSongs = Songs
        //        .Where(s => s.Artist == selectedSong.Artist && s.Title != selectedSong.Title)
        //        .Take(2)
        //        .ToList();
        //}

        public MusicPlayerCache()
        {
            var song1 = new Song
            {
                Index = 1,
                Title = "Beat It",
                Artist = "Michael Jackson",
                AlbumArt = "Images/Beat_It.jpg",
                Liked = "♡",
                Duration = 180,
            };
            var song2 = new Song
            {
                Index = 2,
                Title = "Sweet Home Alabama",
                Artist = "Lynyrd Skynyrd",
                AlbumArt = "Images/Skynyrd-Sweet-Home-Alabama.jpg",
                Liked = "♡",
                Duration = 230,
            };

            var song3 = new Song
            {
                Index = 3,
                Title = "Better Off Alone",
                Artist = "Alice Deejay",
                AlbumArt = "Images/Image-Alice_Deejay_Better_Off_Alone.jpg",
                Liked = "♡",
                Duration = 156,
            };

            var song4 = new Song
            {
                Index = 4,
                Title = "Immigrant Song",
                Artist = "Led Zeppelin",
                AlbumArt = "Images/images.jpg",
                Liked = "♡",
                Duration = 220,
            };

            var song5 = new Song
            {
                Index = 5,
                Title = "Any Way You Want It",
                Artist = "Journey",
                AlbumArt = "Images/Journey_-_Any_Way_You_Want_It_single_cover.jpg",
                Liked = "♡",
                Duration = 200,
            };

            Songs = new List<Song>();
            Songs.Add(song1);
            Songs.Add(song2);
            Songs.Add(song3);
            Songs.Add(song4);
            Songs.Add(song5);

            //SimilarSongs = new List<Song>
            //{
            //    new Song
            //    {
            //        Index = 6,
            //        Title = "Billie Jean",
            //        Artist = "Michael Jackson",
            //        AlbumArt = "Images/BillieJean.jpg",
            //        Liked = "♡",
            //        Duration = 294,
            //    },
            //    new Song
            //    {
            //        Index = 7,
            //        Title = "Thriller",
            //        Artist = "Michael Jackson",
            //        AlbumArt = "Images/Thriller.jpg",
            //        Liked = "♡",
            //        Duration = 357
            //    }
            //};
;
        }
          
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

