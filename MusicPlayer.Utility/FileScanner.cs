using System.Collections.ObjectModel;

namespace MusicPlayer.Utility
{
    //Singleton 
    public class FileScanner
    {
        // Scan and load all song FilePaths into our project.
        private static FileScanner _instance;

        public static FileScanner Instance
        {
            get
            {
               if(_instance == null)
                    _instance = new FileScanner();

               return _instance;
            }
        }

        private FileScanner()
        {

        }

        public ObservableCollection<string> ScanSongs()
        {
            string filePath = @"./Songs";
            var allowedExtensions = new[] { ".mp3", ".m4a" };
            var files = Directory.GetFiles(filePath)
                                 .Where(file => allowedExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                                 .ToList();

            ObservableCollection<string>  songsCollection = new ObservableCollection<string>(files);

            try
            {
                var filePaths = Directory.EnumerateFiles(filePath, "*.*", SearchOption.AllDirectories)
                                         .Where(file => allowedExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)));

                foreach (var path in filePaths)
                {
                    songsCollection.Add(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading files: {ex.Message}");
            }

            return songsCollection;
        }
    }
}
