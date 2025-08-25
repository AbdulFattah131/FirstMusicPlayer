using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    internal class Album
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string CoverImage { get; set; }
        public ObservableCollection<Song> Songs { get; set; } = new();
    }
}
