using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using MusicPlayer;
using NAudio.Wave;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

//               ┌─────────────┐
//               │ Algorithms  │
//               │ (compute /  │  (What)
//               │  next song, │
//               │  shuffle)   │
//               └─────▲───────┘
//                     │
//         outputs →   │
//                     │ informs
//                     │
//         ┌───────────┴───────────┐
//         │                       │
//         ▼                       ▼
//┌─────────────────┐       ┌─────────────────┐
//│ Flow Control    │─────▶ │ Data Structures │
//│ (if playlist    │ reads │ (playlist list, │  (with whom)
//│  empty, skip)   │ from  │  dictionaries)  │
//└─────────▲───────┘       └─────────▲───────┘
// (when    │                         │
// & how)   │ updates / modifies      │ stores / retrieves
//          │                         │
//          ▼                         ▼
//               ┌───────────────┐
//               │ State         │
//               │ Management    │  (where) : time, space
//               │ (current song,│
//               │  index, flags)│
//               └───────▲───────┘
//                       │
//             drives / triggers next Algorithm
//                       │
//                       └─────────────────┐
//                                         │
//                                         ▼
//                                   (back to Algorithms)


//         | Algorithms(A) |            | Flow Control(F) |        |  Data Structures(D) |    | State Management(S) |
//---------| ----------------------     | -------------------------| -------------------------| ----------------------
//Al       | Self - contained logic     | Algorithms drive flow;   | Algorithms rely on       | Algorithms update or
//         | / clarity                  | flow controls execution  | proper structures for    | consume state; state
//         |                            | order and repetition     | efficiency               | affects algorithm outcomes
//---------|---------------------      -|--------------------------|--------------------------|----------------------
//Fc       | Flow directs algs;         | Self - control; loops and| Flow determines how      | Flow triggers state
//         | conditional steps          | conditions manage flow   | data is accessed in time | transitions; state can
//         |                            |                          | and sequence             | change control paths
//---------|----------------------      |--------------------------|--------------------------|----------------------
//Ds       | Structures enable          | Flow uses structures to  | Self-contained storage;  | Data structures hold
//         | algorithms efficiently     | iterate/access data      | structure defines access | state and track history
//         |                            |                          |                          |
//---------|---------------------      -|--------------------------|--------------------------|----------------------
//Sm       | State tracks algorithm     | State affects flow;      | State is stored in       | Self - tracking; updates
//         | progress and results       | triggers loops/conditions| structures               | over time



//Algorithms are the core driver — they rely on data structures and state, and flow control ensures their proper execution.                                   
//Flow control adapts behavior based on algorithms, state, and data organization.

//Data structures are the backbone — they influence and support all other components.

//State management ties everything together across time and operations.


//Music Player App (MVVM) – Core Logic Hierarchy
//│
//├── Layer 1: Architecture & Concurrency
//│   ├── Structure
//│   │   ├── MVVM layers → Model, ViewModel, View
//│   │   ├── Utility & Services → FileScanner, TagReader, SettingsReader
//│   │   └── Project organization → Namespaces, folders
//│   │
//│   ├── Responsibilities
//│   │   ├── Model → Data objects (Track, Playlist, Settings)
//│   │   ├── ViewModel → Commands, data-binding logic, state
//│   │   ├── View → XAML UI definitions
//│   │   └── Services/Utilities → File handling, metadata extraction, persistence
//│   │
//│   ├── Patterns
//│   │   ├── Singleton → SettingsReader (configuration)
//│   │   ├── Observer → INotifyPropertyChanged for data binding
//│   │   ├── Command → RelayCommand for button actions
//│   │   ├── Factory/Builder → Creating Track objects from file scans
//│   │   └── Repository (optional) → For managing playlists or library
//│   │
//│   └── Interactions
//│       ├── View ↔ ViewModel → Data binding, commands
//│       ├── ViewModel ↔ Model → Track/Playlist updates
//│       ├── Services ↔ ViewModel → File scanning, metadata load
//│       └── Async/Threading → Background file scanning, playback
//│
//├── Layer 2: Abstraction & Modeling
//│   └── Types & Abstraction
//│       ├── Track → { Title, Artist, Album, Path, Duration }
//│       ├── Playlist → { Name, Collection<Track> }
//│       ├── FileScanner → abstracts filesystem traversal
//│       ├── TagReader → abstracts audio metadata extraction
//│       └── Settings → user preferences/config
//│
//└── Layer 3: Computational Runtime
//    ├── Sequence
//    │   ├── App startup → Load settings → Scan library → Bind to UI
//    │   ├── User clicks Play → Load file → Stream to audio engine
//    │
//    ├── Selection / Branching
//    │   ├── If file is supported → process metadata
//    │   └── Else → skip / error handling
//    │
//    ├── Iteration / Looping
//    │   ├── For each file in directory → create Track object
//    │   └── For each Track in Playlist → enqueue for playback
//    │
//    ├── Recursion
//    │   └── Recursive folder traversal in FileScanner
//    │
//    └── Termination
//        ├── Closing app → save Settings, release resources
//        └── Stopping playback → dispose audio stream


