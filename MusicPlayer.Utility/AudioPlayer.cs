using NAudio.Wave; 

namespace MusicPlayer.Utility
{
    public class AudioPlayer
    {
        private IWavePlayer _player;
        private AudioFileReader _audioFile;
        public bool IsPlaying => _player?.PlaybackState == PlaybackState.Playing;


        public AudioPlayer()
        {
            _player = new WaveOutEvent();
        }

        public void Load(string filePath)
        {
            _audioFile?.Dispose();
            _audioFile = new AudioFileReader(filePath);
            _player.Init(_audioFile);
        }

        public void Play()
        {
            _player?.Play();
        }

        public void Pause()
        {
            _player?.Pause();
        }

        public void Stop()
        {
            _player?.Stop();

        }

    }


}

