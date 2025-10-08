using System.Collections.ObjectModel;
using MusicPlayer.Data.Objects;

namespace MusicPlayer.Utility
{
    public class TagReader
    {
        private static TagReader _instance; // tagreader instance
        public static TagReader Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new TagReader();

                return _instance;
            }
        }

        private Dictionary<string, Album> m_dictAlbums = new Dictionary<string, Album>(); // dictionary of albums

        public Song ReadSongFromFilePath(string stfilePath) // read song from file path
        {
            Song song = null;

            using (TagLib.File file = TagLib.File.Create(stfilePath))
            {
                if (!m_dictAlbums.ContainsKey(file.Tag.Album))
                {
                    Album album = new Album()
                    {
                        Title = file.Tag.Album,
                        Artist = string.Join(", ", file.Tag.AlbumArtists),
                        Year = (int)file.Tag.Year,
                        ImageData = file.Tag.Pictures[0].Data.Data,
                    };

                    foreach (string genre in file.Tag.Genres)
                    {
                        album.Genres.Add(genre);
                    }

                    m_dictAlbums.Add(file.Tag.Album, album);
                }

                song = new Song
                {
                    Title = file.Tag.Title,
                    Artist = string.Join(", ", file.Tag.Performers),
                    Album = m_dictAlbums[file.Tag.Album],
                    Genre = string.Join(", ", file.Tag.Genres),
                    Length = file.Tag.Length,
                    TrackNumber = (int)file.Tag.Track,
                    FilePath = stfilePath
                };
                m_dictAlbums[file.Tag.Album].Songs.Add(song);
            }

            return song;
        }

        public ObservableCollection<Song> ReadSongsFromFilePaths(IList<string> lstFilePaths)
        {
            ObservableCollection<Song> lstSongs = new ObservableCollection<Song>();

            // add each song
            foreach (string stFilePath in lstFilePaths)
            {
                Song temp = ReadSongFromFilePath(stFilePath);

                if (temp != null)
                {
                    lstSongs.Add(temp);
                }
            }
         
            return lstSongs;
        }
        public IEnumerable<Album> GetAlbums()
        {
            return m_dictAlbums.Values;
        }
        private TagReader()
        {
        }
    }
}
