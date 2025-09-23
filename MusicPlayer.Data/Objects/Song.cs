using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        private string filePath;
        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        public string isLiked { get; set; }
        public string ImagePath => Album?.ImagePath;
        public int Duration { get; set; }
        public int PlayCount { get; set; }

    }
}
