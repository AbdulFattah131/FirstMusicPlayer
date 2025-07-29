using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Data.Objects;
using System.Xml.Serialization;

namespace MusicPlayer.Utility
{
    public class ThemeReader
    {
            public Theme LoadFromFile(string path)
            {
                if (!File.Exists(path))
                    return null;

                var serializer = new XmlSerializer(typeof(Theme));
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    return (Theme)serializer.Deserialize(fs);
                }
            }
        }
    }
}
