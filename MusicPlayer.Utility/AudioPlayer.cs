using System.ComponentModel;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace MusicPlayer.Utility
{
    public class AudioPlayer :INotifyPropertyChanged
    {
        private IWavePlayer _player;
        private AudioFileReader _audioFile;
        private WaveStream _stream;
        private string _loadedFilePath;
        private MMDevice _defaultDevice;
        private float _systemVolume;
        private bool _isMuted;
        public bool IsPlaying => _player?.PlaybackState == PlaybackState.Playing;
        
        public AudioPlayer()
        {
            _player = new WaveOutEvent();
        }

        public void InitializeVolume()
        {
            var enumerator = new MMDeviceEnumerator();
            _defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            _systemVolume = _defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
            _isMuted = _defaultDevice.AudioEndpointVolume.Mute;

            _defaultDevice.AudioEndpointVolume.OnVolumeNotification += (data) =>
            {
                SystemVolume = data.MasterVolume;
                IsMuted = data.Muted;
            };

        }
        public float SystemVolume
        {
            get => _systemVolume;
            set
            {
                if (Math.Abs(value - _systemVolume) > 0.001f)
                {
                    _systemVolume = value;
                    if (_defaultDevice != null)
                        _defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value;
                }

                OnPropertyChanged(nameof(SystemVolume));
            }
        }
        public bool IsMuted
        {
            get => _isMuted;
            set
            {
                if (_isMuted != value)
                {
                    _isMuted = value;
                    if (_defaultDevice != null)
                        _defaultDevice.AudioEndpointVolume.Mute = value;
                }
            }
        }
        public void Load(string filePath)
        {
            Stop();

            _audioFile = new AudioFileReader(filePath);
            _player.Init(_audioFile);
            _loadedFilePath = filePath;
        }

        WaveOutEvent WaveOut = new WaveOutEvent();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
