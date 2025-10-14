using MusicPlayer.Data.Objects;
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
            Stop();

            _audioFile = new AudioFileReader(filePath);
            _player.Init(_audioFile);
        }

        WaveOutEvent WaveOut = new WaveOutEvent();

        public void TogglePlayPause()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
        public void Shuffle(string filePath)
        {

        }

        public void Repeat(string filePath)
        {

        }

        public void Pause()
        {
            _player?.Pause();
        }

        public void Play()
        {
            _player?.Play();
        }
        
        public void Stop()
        {
            _player?.Stop();
            _audioFile?.Dispose();
            _audioFile = null;
        }

        public void SetOutputDevice(int deviceNumber)
        {
            _player?.Stop();
            _player?.Dispose();

            _player = new WaveOutEvent() { DeviceNumber = deviceNumber };

            if (_audioFile != null)
            {
                _player.Init(_audioFile);
            }
        }
    }
}
