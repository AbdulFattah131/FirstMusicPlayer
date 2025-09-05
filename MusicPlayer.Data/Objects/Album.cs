using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    public class Album : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ImagePath { get; set; }
        public int ReleaseDate { get; set; }
        public string Genre { get; set; }

        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();

        private string albumInfo;
        public string AlbumInfo
        {
            get => albumInfo;
            set
            {
                albumInfo = value;
                OnPropertyChanged(nameof(AlbumInfo));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

