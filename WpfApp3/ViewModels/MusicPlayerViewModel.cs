using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicPlayer.Data.Objects;

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
        public ObservableCollection<Album> Albums { get; set; }

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

        public MusicPlayerCache()
        {
            var album1 = new Album { Title = "Thriller", Artist = "Michael Jackson", ReleaseDate = 1982, ImagePath= "/Images/Michael_Jackson_-_Thriller.png" };

            album1.Songs.Add(new Song { Title = "Beat It", TrackNumber = 1, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson" });
            album1.Songs.Add(new Song { Title = "Wanna Be Startin' Somethin'", TrackNumber = 2, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song { Title = "Baby Be Mine", TrackNumber = 3, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "The Girl Is Mine(featuring Paul McCartney)", TrackNumber = 4, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson, Paul McCartney", });
            album1.Songs.Add(new Song {Title = "Thriller", TrackNumber = 5, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "Billie Jean", TrackNumber = 6, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "Human Nature", TrackNumber = 7, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "P.Y.T. (Pretty Young Thing)", TrackNumber = 8, Album = album1, Liked = "♡", Duration = 180, Artist = "Michael Jackson", });

            var album2 = new Album { Title = "Second Helping", Artist = "Lynyrd Skynyrd", ReleaseDate = 1974, ImagePath= "/Images/SecondHelpingLynyrdSkynyrd.jpg" };

            album2.Songs.Add(new Song { Title = "Sweet Home Alabama", TrackNumber = 1, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd" });
            album2.Songs.Add(new Song { Title = "I Need You", TrackNumber = 2, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });
            album2.Songs.Add(new Song { Title = "Don't Ask Me No Questions", TrackNumber = 3, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });
            album2.Songs.Add(new Song { Title = "Workin' for MCA", TrackNumber = 4, Album = album2, Liked = "♡", Duration = 180, Artist = "Michael Jackson, Paul McCartney", });
            album2.Songs.Add(new Song { Title = "The Ballad of Curtis Loew", TrackNumber = 5, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });
            album2.Songs.Add(new Song { Title = "Swamp Music", TrackNumber = 6, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });
            album2.Songs.Add(new Song { Title = "The Needle and The Spoon", TrackNumber = 7, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });
            album2.Songs.Add(new Song { Title = "Call Me the Breeze", TrackNumber = 8, Album = album2, Liked = "♡", Duration = 180, Artist = "Lynyrd Skynyrd", });

            var album3 = new Album { Title = "Who Needs Guitars Anyway?", Artist = "Alice Deejay", ReleaseDate = 2000, ImagePath= "/Images/Who_Needs_Guitars_Anyway_album_cover_by_Alice_DeeJay.jpg" };

            album3.Songs.Add(new Song { Title = "Celebrate Our Love", TrackNumber = 1, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay" });
            album3.Songs.Add(new Song { Title = "The Lonely One", TrackNumber = 2, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });
            album3.Songs.Add(new Song { Title = "Will I Ever", TrackNumber = 3, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });
            album3.Songs.Add(new Song { Title = "Elements of Life", TrackNumber = 4, Album = album3, Liked = "♡", Duration = 180, Artist = "Michael Jackson, Paul McCartney", });
            album3.Songs.Add(new Song { Title = "Back in My Life", TrackNumber = 5, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });
            album3.Songs.Add(new Song { Title = "Better Off Alone", TrackNumber = 6, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });
            album3.Songs.Add(new Song { Title = "No More Lies", TrackNumber = 7, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });
            album3.Songs.Add(new Song { Title = "Everything Begins With An E", TrackNumber = 8, Album = album3, Liked = "♡", Duration = 180, Artist = "Alice Deejay", });

            var album4 = new Album { Title = "Led Zeppelin III", Artist = "Led Zeppelin", ReleaseDate = 1970, ImagePath= "/Images/Led_Zeppelin_-_Led_Zeppelin_III.png" };

            album4.Songs.Add(new Song { Title = "Immigrant Song", TrackNumber = 1, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin" });
            album4.Songs.Add(new Song { Title = "Friends", TrackNumber = 2, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Celebration Day", TrackNumber = 3, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Since I've Been Loving You", TrackNumber = 4, Album = album4, Liked = "♡", Duration = 180, Artist = "Michael Jackson, Paul McCartney", });
            album4.Songs.Add(new Song { Title = "Out on the Tiles", TrackNumber = 5, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Gallows Pole", TrackNumber = 6, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Tangerine", TrackNumber = 7, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "That's the Way", TrackNumber = 8, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Bron-Y-Aur Stomp", TrackNumber = 9, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Hats Off To (Roy) Harper", TrackNumber = 10, Album = album4, Liked = "♡", Duration = 180, Artist = "Led Zeppelin", });

            var album5 = new Album { Title = "Adele's 21", Artist = "Adele", ReleaseDate = 2012, ImagePath= "/Images/Adele_-_21.png" };

            album5.Songs.Add(new Song { Title = "Rolling in the Deep", TrackNumber = 1, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele" });
            album5.Songs.Add(new Song { Title = "Rumour Has It", TrackNumber = 2, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Turning Tables", TrackNumber = 3, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Set Fire to the Rain", TrackNumber = 4, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Someone Like You", TrackNumber = 5, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Don't You Remember", TrackNumber = 6, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "He Won't Go", TrackNumber = 7, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Take It All", TrackNumber = 8, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "I'll Be Waiting", TrackNumber = 9, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "One and Only", TrackNumber = 10, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Lovesong", TrackNumber = 11, Album = album5, Liked = "♡", Duration = 180, Artist = "Adele", });

            Albums = new ObservableCollection<Album>();
            Albums.Add(album1);
            Albums.Add(album2);
            Albums.Add(album3);
            Albums.Add(album4);
            Albums.Add(album5);
        }
          
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

