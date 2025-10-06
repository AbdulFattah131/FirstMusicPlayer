using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer.Data.Objects
{
    public class Album : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ImagePath { get; set; }
        public int ReleaseDate { get; set; }


        private HashSet<string> _hsGenres = new();
        public HashSet<string> Genres
        {
                get => _hsGenres;
        }
        public string Genre
        {
                get => string.Join(" , ", Genres);
        }
        
       
        public int Year { get; set; }   
        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

