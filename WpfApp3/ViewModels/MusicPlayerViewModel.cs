using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            var album1 = new Album { Title = "Thriller", Artist = "Michael Jackson", ReleaseDate = 1982, ImagePath= "/Images/Michael_Jackson_-_Thriller.png", AlbumInfo= "Released in 1982, Thriller is the best-selling album of all time, produced by Quincy Jones and featuring a mix of pop, rock, and R&B. It spawned hit singles like \"Billie Jean\" and \"Beat It,\" and its innovative music videos transformed the medium into an art form, cementing Jackson's \"King of Pop\" status." };

            album1.Songs.Add(new Song { Title = "Beat It", TrackNumber = 1, Album = album1, Liked = "♡", Duration = 258, Artist = "Michael Jackson" });
            album1.Songs.Add(new Song { Title = "Wanna Be Startin' Somethin'", TrackNumber = 2, Album = album1, Liked = "♡", Duration = 363, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song { Title = "Baby Be Mine", TrackNumber = 3, Album = album1, Liked = "♡", Duration = 260, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "The Girl Is Mine", TrackNumber = 4, Album = album1, Liked = "♡", Duration = 222, Artist = "Michael Jackson, Paul McCartney", });
            album1.Songs.Add(new Song {Title = "Thriller", TrackNumber = 5, Album = album1, Liked = "♡", Duration = 357, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "Billie Jean", TrackNumber = 6, Album = album1, Liked = "♡", Duration = 294, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "Human Nature", TrackNumber = 7, Album = album1, Liked = "♡", Duration = 246, Artist = "Michael Jackson", });
            album1.Songs.Add(new Song {Title = "P.Y.T. (Pretty Young Thing)", TrackNumber = 8, Album = album1, Liked = "♡", Duration = 239, Artist = "Michael Jackson", });

            var album2 = new Album { Title = "Rock or Bust", Artist = "AC/DC", ReleaseDate = 2014, ImagePath= "/Images/Rock_or_Bust.jpg", AlbumInfo= "Rock or Bust is the fifteenth studio album by the Australian hard rock band AC/DC, released in 2014. It is notable for being the band's first studio album in six years and for being the first in their 41-year history without founding rhythm guitarist Malcolm Young due to illness, with his nephew, Stevie Young, filling in for him." };

            album2.Songs.Add(new Song { Title = "Rock or Bust", TrackNumber = 1, Album = album2, Liked = "♡", Duration = 183, Artist = "AC/DC" });
            album2.Songs.Add(new Song { Title = "Play Ball", TrackNumber = 2, Album = album2, Liked = "♡", Duration = 166, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Miss Adventure", TrackNumber = 3, Album = album2, Liked = "♡", Duration = 174, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Dogs of War", TrackNumber = 4, Album = album2, Liked = "♡", Duration = 215, Artist = "Michael Jackson, Paul McCartney", });
            album2.Songs.Add(new Song { Title = "Hard Times", TrackNumber = 5, Album = album2, Liked = "♡", Duration = 164, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Rock the House", TrackNumber = 6, Album = album2, Liked = "♡", Duration = 162, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Rock the Blues Away", TrackNumber = 7, Album = album2, Liked = "♡", Duration = 204, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Got Some Rock N' Roll Thunder", TrackNumber = 8, Album = album2, Liked = "♡", Duration = 202, Artist = "AC/DC", });
            album2.Songs.Add(new Song { Title = "Emission Control", TrackNumber = 9, Album = album2, Liked = "♡", Duration = 221, Artist = "AC/DC", });

            var album3 = new Album { Title = "Funk Wav Bounces Vol. 1", Artist = "Calvin Harris", ReleaseDate = 2017, ImagePath= "/Images/Funk_Wav_Bounces_1.jpg", AlbumInfo= "It is the fifth studio album by Scottish DJ and producer Calvin Harris, released on June 30, 2017. The album features a significant departure from Harris's typical dance-pop sound, embracing a sun-kissed, laid-back funk and R&B style filled with island vibes and relaxed grooves." };

            album3.Songs.Add(new Song { Title = "Slide", TrackNumber = 1, Album = album3, Liked = "♡", Duration = 230, Artist = "Calvin Harris" });
            album3.Songs.Add(new Song { Title = "Heatstroke", TrackNumber = 2, Album = album3, Liked = "♡", Duration = 229, Artist = "Calvin Harris" });
            album3.Songs.Add(new Song { Title = "Feels", TrackNumber = 3, Album = album3, Liked = "♡", Duration = 223, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Cash Out", TrackNumber = 4, Album = album3, Liked = "♡", Duration = 236, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Rollin", TrackNumber = 5, Album = album3, Liked = "♡", Duration = 272, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Prayers Up", TrackNumber = 6, Album = album3, Liked = "♡", Duration = 204, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Holiday", TrackNumber = 7, Album = album3, Liked = "♡", Duration = 169, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Faking It", TrackNumber = 8, Album = album3, Liked = "♡", Duration = 240, Artist = "Calvin Harris", });
            album3.Songs.Add(new Song { Title = "Hard To Love", TrackNumber = 9, Album = album3, Liked = "♡", Duration = 230, Artist = "Calvin Harris", });

            var album4 = new Album { Title = "Mothership [Disc 1]", Artist = "Led Zeppelin", ReleaseDate = 1970, ImagePath= "/Images/Led_Zeppelin_-_Mothership.jpg", AlbumInfo= "It is the 1970 third studio album by Led Zeppelin, featuring a significant shift towards folk and acoustic music, inspired by the band's time writing in a Welsh cottage. Released on October 5, 1970, by Atlantic Records, it showcases the band's diverse musicianship, with John Paul Jones playing a wider range of instruments and Jimmy Page producing." };

            album4.Songs.Add(new Song { Title = "Immigrant Song", TrackNumber = 1, Album = album4, Liked = "♡", Duration = 147, Artist = "Led Zeppelin" });
            album4.Songs.Add(new Song { Title = "Good Times Bad Times", TrackNumber = 2, Album = album4, Liked = "♡", Duration = 168, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Communication Breakdown", TrackNumber = 3, Album = album4, Liked = "♡", Duration = 150, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Dazed And Confused", TrackNumber = 4, Album = album4, Liked = "♡", Duration = 388, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Ramble On", TrackNumber = 5, Album = album4, Liked = "♡", Duration = 268, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Heartbreaker", TrackNumber = 6, Album = album4, Liked = "♡", Duration = 256, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Rock And Roll", TrackNumber = 7, Album = album4, Liked = "♡", Duration = 221, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "When The Levee Breaks", TrackNumber = 8, Album = album4, Liked = "♡", Duration = 430, Artist = "Led Zeppelin", });
            album4.Songs.Add(new Song { Title = "Stairway To Heaven", TrackNumber = 9, Album = album4, Liked = "♡", Duration = 481, Artist = "Led Zeppelin", });

            var album5 = new Album { Title = "Adele's 21", Artist = "Adele", ReleaseDate = 2012, ImagePath= "/Images/Adele_-_21.png", AlbumInfo= "Adele's \"21\" is a 2011 studio album named for her age during its creation, inspired by heartbreak and a blend of soul, blues, and country music, winning the Grammy for Album of the Year, and solidifying Adele's status as a music icon." };

            //List<Songs> lstSongs = SongReader.Read()
            //List<Songs> lstSongs go through;
            //Dictionary<string, List<Songs>> 
            //Dictionary.Add(21, song), 21, song

            album5.Songs.Add(new Song { Title = "Rolling in the Deep", TrackNumber = 1, Album = album5, Liked = "♡", Duration = 228, Artist = "Adele" });
            album5.Songs.Add(new Song { Title = "Rumour Has It", TrackNumber = 2, Album = album5, Liked = "♡", Duration = 223, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Turning Tables", TrackNumber = 3, Album = album5, Liked = "♡", Duration = 250, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Set Fire to the Rain", TrackNumber = 4, Album = album5, Liked = "♡", Duration = 242, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Someone Like You", TrackNumber = 5, Album = album5, Liked = "♡", Duration = 285, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Don't You Remember", TrackNumber = 6, Album = album5, Liked = "♡", Duration = 243, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "He Won't Go", TrackNumber = 7, Album = album5, Liked = "♡", Duration = 278, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Take It All", TrackNumber = 8, Album = album5, Liked = "♡", Duration = 228, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "I'll Be Waiting", TrackNumber = 9, Album = album5, Liked = "♡", Duration = 241, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "One and Only", TrackNumber = 10, Album = album5, Liked = "♡", Duration = 348, Artist = "Adele", });
            album5.Songs.Add(new Song { Title = "Lovesong", TrackNumber = 11, Album = album5, Liked = "♡", Duration = 316, Artist = "Adele", });

            var album6 = new Album { Title = "Uptown Special", Artist = "Mark Ronson", ReleaseDate = 2015, ImagePath = "/Images/Mark_Ronson_-_Uptown_Special_(Official_Album_Cover).png", AlbumInfo = "Uptown Special is the fourth studio album by British DJ and producer Mark Ronson. The album was released on 13 January 2015 in the US and 19 January 2015 in the UK. Ronson dedicated the album to the late Amy Winehouse." };

            album6.Songs.Add(new Song { Title = "Uptown's First Finale", TrackNumber = 1, Album = album6, Liked = "♡", Duration = 57, Artist = "Mark Ronson" });
            album6.Songs.Add(new Song { Title = "Uptown Funk", TrackNumber = 2, Album = album6, Liked = "♡", Duration = 270, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "I Can't Lose", TrackNumber = 3, Album = album6, Liked = "♡", Duration = 197, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Crack In The Pearl", TrackNumber = 4, Album = album6, Liked = "♡", Duration = 129, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Leaving Los Feliz", TrackNumber = 5, Album = album6, Liked = "♡", Duration = 280, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Heavy And Rolling", TrackNumber = 6, Album = album6, Liked = "♡", Duration = 204, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Crack In The Pearl Pt. II", TrackNumber = 7, Album = album6, Liked = "♡", Duration = 138, Artist = "Mark Ronson    ", });
            album6.Songs.Add(new Song { Title = "Summer Breaking", TrackNumber = 8, Album = album6, Liked = "♡", Duration = 246, Artist =   "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Feel Right", TrackNumber = 9, Album = album6, Liked = "♡", Duration = 223, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "Daffodils", TrackNumber = 10, Album = album6, Liked = "♡", Duration = 298, Artist = "Mark Ronson", });
            album6.Songs.Add(new Song { Title = "In Case Of Fire", TrackNumber = 11, Album = album6, Liked = "♡", Duration = 274, Artist = "Mark Ronson", });

            var album7 = new Album { Title = "The Kids Are Coming", Artist = "Tones and I", ReleaseDate = 2019, ImagePath = "/Images/The_Kids_Are_Coming_by_Tones_and_I.png", AlbumInfo = "The Kids Are Coming is the debut extended play by Australian singer Tones and I. The EP was released on 30 August 2019.The EP was announced on 16 July 2019, alongside the release of single \"Never Seen the Rain\". At the ARIA Music Awards of 2019, the EP won ARIA Award for Best Independent Release. At the AIR Awards of 2020, the EP was nominated for Best Independent Pop Album or EP." };

            album7.Songs.Add(new Song { Title = "The Kids Are Coming", TrackNumber = 1, Album = album7, Liked = "♡", Duration = 186, Artist = "Tones and I" });
            album7.Songs.Add(new Song { Title = "Dance Monkey", TrackNumber = 2, Album = album7, Liked = "♡", Duration = 209, Artist = "Tones and I", });
            album7.Songs.Add(new Song { Title = "Colourblind", TrackNumber = 3, Album = album7, Liked = "♡", Duration = 227, Artist = "Tones and I", });
            album7.Songs.Add(new Song { Title = "Johnny Run Away", TrackNumber = 4, Album = album7, Liked = "♡", Duration = 210, Artist = "Tones and I", });
            album7.Songs.Add(new Song { Title = "Jimmy", TrackNumber = 5, Album = album7, Liked = "♡", Duration = 205, Artist = "Tones and I", });
            album7.Songs.Add(new Song { Title = "Never Seen the Rain", TrackNumber = 6, Album = album7, Liked = "♡", Duration = 211, Artist = "Tones and I", });

            var album8 = new Album { Title = "The Dark Side of the Moon", Artist = "Pink Floyd", ReleaseDate = 1973, ImagePath = "/Images/Dark_Side_of_the_Moon.png", AlbumInfo = "The Dark Side of the Moon is the eighth studio album by the English rock band Pink Floyd, released on 1 March 1973 by Capitol Records in the US and on 16 March 1973 by Harvest Records in the UK. Developed during live performances before recording began, it was conceived as a concept album that would focus on the pressures faced by the band during their arduous lifestyle, and also deal with the mental health problems of the former band member Syd Barrett, who had departed the group in 1968." };

            album8.Songs.Add(new Song { Title = "Speak To Me", TrackNumber = 1, Album = album8, Liked = "♡", Duration = 90, Artist = "Pink Floyd" });
            album8.Songs.Add(new Song { Title = "Breathe", TrackNumber = 2, Album = album8, Liked = "♡", Duration = 163, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "On The Run", TrackNumber = 3, Album = album8, Liked = "♡", Duration = 215, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "Time", TrackNumber = 4, Album = album8, Liked = "♡", Duration = 421, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "The Great Gig In The Sky", TrackNumber = 5, Album = album8, Liked = "♡", Duration = 276, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "Money", TrackNumber = 6, Album = album8, Liked = "♡", Duration = 382, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "Us And Them", TrackNumber = 7, Album = album8, Liked = "♡", Duration = 462, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "Any Colour You Like", TrackNumber = 8, Album = album8, Liked = "♡", Duration = 205, Artist = "Pink Floyd", });
            album8.Songs.Add(new Song { Title = "Eclipse", TrackNumber = 9, Album = album8, Liked = "♡", Duration = 123, Artist = "Pink Floyd", });

            Albums = new ObservableCollection<Album>();
            Albums.Add(album1);
            Albums.Add(album2);
            Albums.Add(album3);
            Albums.Add(album4);
            Albums.Add(album5);
            Albums.Add(album6);
            Albums.Add(album7);
            Albums.Add(album8);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

