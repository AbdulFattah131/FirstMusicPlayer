using System;                
using System.Collections.Generic; 
using System.IO;            
using System.Linq; 

namespace MusicPlayer
{
    public static class FileScanner
    {
        public static List<string> ScanSongs(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The folder '{folderPath}' was not found.");

            var files = Directory.GetFiles(folderPath, "*.mp3");

            return files.ToList();
        }
    }
}
