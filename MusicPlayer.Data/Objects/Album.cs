using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    public class Album
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ImagePath { get; set; }
        public int ReleaseDate { get; set; }  
        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();
    }
}
