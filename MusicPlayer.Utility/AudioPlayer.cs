using MusicPlayer.Data.Objects;
using NAudio.Wave;
using NAudio.Utils;
using NAudio.MediaFoundation;
using NAudio.CoreAudioApi;

namespace MusicPlayer.Utility
{
    public class AudioPlayer
    {
        private IWavePlayer _player;
        private AudioFileReader _audioFile;
        private WaveStream _stream;
        private string _loadedFilePath;
        private readonly MMDevice _mMDevice;
        public bool IsPlaying => _player?.PlaybackState == PlaybackState.Playing;
        
        public AudioPlayer()
        {
            _player = new WaveOutEvent();
        }

        public MMDevice MMDevice;

        public void Load(string filePath)
        {
            Stop();

            _audioFile = new AudioFileReader(filePath);
            _player.Init(_audioFile);
            _loadedFilePath = filePath;
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
        public TimeSpan CurrentTime
        {
            get
            {
                if (_audioFile == null)
                    return TimeSpan.Zero;
                return _audioFile.CurrentTime;
            }
            set
            {
                if (_audioFile != null)
                    _audioFile.CurrentTime = value;
            }
        }

        public TimeSpan TotalTime
        {
            get
            {
                if (_audioFile == null)
                    return TimeSpan.Zero;
                return _audioFile.TotalTime;
            }
        }

        public void Seek(double progress)
        {
            if (_audioFile != null && progress >= 0 && progress <= 1)
            {
                _audioFile.CurrentTime = TimeSpan.FromSeconds(_audioFile.TotalTime.TotalSeconds * progress);
            }
        }


    }
}
