using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Data.Objects
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArt { get; set; }  // Path to image
        public string FilePath { get; set; }  // Path to audio file
    }
}
