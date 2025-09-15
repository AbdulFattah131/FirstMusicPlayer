using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media; // WPF MediaPlayer

namespace MusicPlayer.Services
{
    public class AudioService
    {
        private MediaPlayer mediaPlayer; // WPF built-in media player
        private List<string> playlist;   // list of audio file paths
        private int currentIndex;

        public AudioService()
        {
            mediaPlayer = new MediaPlayer();
            playlist = new List<string>();
            currentIndex = 0;

            // Optional: handle media ended event
            mediaPlayer.MediaEnded += (s, e) => PlayNext();
        }

        // Load files into playlist
        public void LoadPlaylist(List<string> audioFiles)
        {
            playlist.Clear();
            foreach (var file in audioFiles)
            {
                if (File.Exists(file))
                    playlist.Add(file);
            }
            currentIndex = 0;
        }

        // Play current track
        public void Play()
        {
            if (playlist.Count == 0) return;

            mediaPlayer.Open(new Uri(playlist[currentIndex], UriKind.Absolute));
            mediaPlayer.Play();
        }

        // Pause
        public void Pause()
        {
            mediaPlayer.Pause();
        }

        // Stop
        public void Stop()
        {
            mediaPlayer.Stop();
        }

        // Play next track
        public void PlayNext()
        {
            if (playlist.Count == 0) return;

            currentIndex = (currentIndex + 1) % playlist.Count;
            Play();
        }

        // Play previous track
        public void PlayPrevious()
        {
            if (playlist.Count == 0) return;

            currentIndex = (currentIndex - 1 + playlist.Count) % playlist.Count;
            Play();
        }

        // Set volume (0.0 to 1.0)
        public void SetVolume(double volume)
        {
            mediaPlayer.Volume = Math.Clamp(volume, 0.0, 1.0);
        }

        // Get current track path
        public string GetCurrentTrack()
        {
            if (playlist.Count == 0) return null;
            return playlist[currentIndex];
        }
    }
}
