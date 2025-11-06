using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    public class Playlist : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public ObservableCollection<Song> PlaylistSongs { get; set; } = new ObservableCollection<Song>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
