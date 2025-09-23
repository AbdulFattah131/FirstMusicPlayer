using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;
using TagLib;

namespace MusicPlayer.Utility
{
    public class TagReader
    {
        public TagReader() 
        {
            var a = FileScanner.Instance.ScanSongs();

            foreach (var path in a)
            {
                using (TagLib.File file = TagLib.File.Create(path))
                {
                    string title = file.Tag.Title;
                    string artist = string.Join(", ", file.Tag.Performers);
                    string album = file.Tag.Album;

                }
            }
        }
        
    }
}
