using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    public class Song
    {
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public Album Album { get; set; }
        public string FilePath { get; set; }
        public string Liked { get; set; }
        public int Duration { get; set; }
        public int PlayCount { get; set; }
    }
}
