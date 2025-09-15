using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

public class AudioService
{
    private IWavePlayer waveOut;               // Audio playback device
    private AudioFileReader audioFile;         // Current audio file
    private List<string> playlist;             // List of audio file paths
    private int currentIndex;                  // Index of currently playing track

    public AudioService()
    {
        playlist = new List<string>();
        currentIndex = 0;
    }

    public void LoadPlaylist(List<string> files)
    {
        playlist.Clear();
        foreach (var file in files)
        {
            if (File.Exists(file))
                playlist.Add(file);
        }
        currentIndex = 0;
    }

    public void Play()
    {
        if (playlist.Count == 0) return;

        Stop(); // Stop current audio if any

        string filePath = playlist[currentIndex];
        if (File.Exists(filePath))
        {
            audioFile = new AudioFileReader(filePath);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFile);
            waveOut.PlaybackStopped += OnPlaybackStopped;
            waveOut.Play();
        }
    }

    public void Play(string filePath)
    {
        if (!File.Exists(filePath)) return;

        Stop();

        audioFile = new AudioFileReader(filePath);
        waveOut = new WaveOutEvent();
        waveOut.Init(audioFile);
        waveOut.PlaybackStopped += OnPlaybackStopped;
        waveOut.Play();
    }

    public void Pause()
    {
        waveOut?.Pause();
    }

    public void Resume()
    {
        waveOut?.Play();
    }

    public void Stop()
    {
        if (waveOut != null)
        {
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
        }

        if (audioFile != null)
        {
            audioFile.Dispose();
            audioFile = null;
        }
    }

    public void Next()
    {
        if (playlist.Count == 0) return;

        currentIndex++;
        if (currentIndex >= playlist.Count) currentIndex = 0;
        Play();
    }

    public void Previous()
    {
        if (playlist.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0) currentIndex = playlist.Count - 1;
        Play();
    }

    private void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        Next();
    }

    public string CurrentTrackName()
    {
        if (playlist.Count == 0) return null;
        return Path.GetFileName(playlist[currentIndex]);
    }
}
