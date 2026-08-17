using UnityEngine;
using System;

public static class AudioManager
{
    public static AudioTrack[] Tracks = new AudioTrack[8];

    public static GameObject SourceObject;

    public static float AudioVolume => Options.Volume;

    private static bool Initiated = false;

    public static void Initiate()
    {
        SourceObject = new GameObject("AudioTracks");

        for (int i = 0; i < Tracks.Length; i++)
        {
            Tracks[i] = new AudioTrack();
        }

        Initiated = true;
    }

    public static void UpdateVolumes()
    {
        if(Initiated == false)
        {
            return;
        }

        for (int i = 0; i < Tracks.Length; i++)
        {
            Tracks[i].Volume = Options.Volume;
        }
    }

    public static void SetSound(AudioPreset sound)
    {
        if (Initiated == false)
        {
            return;
        }

        if (sound == null)
        {
            return;
        }

        if (sound.Sound != null)
        {
            Tracks[sound.TrackSlot].SetSound(sound.Sound);
        }

        Tracks[sound.TrackSlot].Volume = sound.Volume * AudioVolume;
        Tracks[sound.TrackSlot].Pitch = sound.Pitch;

        Tracks[sound.TrackSlot].Loop = sound.Looping;
        Tracks[sound.TrackSlot].Mute = sound.Muted;
    }

    public static void PlaySound(AudioPreset sound)
    {
        if (Initiated == false)
        {
            return;
        }

        SetSound(sound);
        Tracks[sound.TrackSlot].Play();
    }
}

public class AudioTrack
{
    private AudioSource Source;

    private float volume = 0;
    public float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
            Source.volume = volume;
        }
    }

    private float pitch = 1.0f;
    public float Pitch
    {
        get
        {
            return pitch;
        }
        set
        {
            pitch = value;
            Source.pitch = pitch;
        }
    }

    private bool loop;
    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
            Source.loop = loop;
        }
    }

    private bool mute;
    public bool Mute
    {
        get
        {
            return mute;
        }
        set
        {
            mute = value;
            Source.mute = mute;
        }
    }

    public AudioTrack()
    {
        Source = AudioManager.SourceObject.AddComponent<AudioSource>();
        Source.playOnAwake = false;
    }

    public void Play()
    {
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void SetSound(AudioClip sound)
    {
        Source.clip = sound;
    }
}

[Serializable]
public class AudioPreset
{
    public string Name;

    public AudioClip Sound;

    public float Volume;
    public float Pitch;

    public bool Looping;
    public bool Muted;

    public int TrackSlot;
}