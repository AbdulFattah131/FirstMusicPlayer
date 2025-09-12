using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Data.Objects;

namespace MusicPlayer.UIComponents.ViewModels
{
    public class MusicRepository
    {
        private readonly string musicFolder;

        public MusicRepository(string musicFolder)
        {
            this.musicFolder = musicFolder;
        }

        public List<Album> GetAlbums()
        {
            var albums = new List<Album>();

            // For simplicity, assume each folder in musicFolder is an album
            foreach (var dir in Directory.GetDirectories(musicFolder))
            {
                var album = new Album
                {
                    Id = dir.GetHashCode(), // simple unique ID
                    Title = Path.GetFileName(dir),
                    Artist = "Unknown Artist", // optionally read metadata
                    CoverPath = Directory.GetFiles(dir, "*.jpg").FirstOrDefault() ?? ""
                };
                albums.Add(album);
            }

            return albums;
        }
    }
}
