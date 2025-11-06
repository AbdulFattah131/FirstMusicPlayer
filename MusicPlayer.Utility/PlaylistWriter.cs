using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MusicPlayer.Data.Objects;

namespace MusicPlayer.Utility
{
    public class PlaylistWriter
    {
        private static PlaylistWriter _Instance;
        public static PlaylistWriter Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new PlaylistWriter();
                return _Instance;
            }
        }

        private PlaylistWriter() { }

        public void WriteToFile(List<Playlist> playlists)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var serializer = new XmlSerializer(typeof(List<Playlist>));
                serializer.Serialize(writer, playlists);
                writer.Flush();

                string folderPath = "./";
                string filePath = $"{folderPath}/Playlists.xml";

                File.WriteAllBytes(filePath, ms.ToArray());
            }
        }
    }
}
