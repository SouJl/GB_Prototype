using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [NonReorderable]
    public List<Sound> sounds;

    public static SoundManager instance;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.SpatialBlend;
            s.source.reverbZoneMix = s.reverZoneMix;
            s.source.dopplerLevel = s.dopplerLevel;
            s.source.spread = s.spread;
            s.source.rolloffMode = s.RolloffMode;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.MaxDistance;
        }
    }

    public void Play(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.Play();
    }

    public void Pause(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.Pause();
    }

    public void Stop(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.Stop();
    }

    public float GetSoundLength(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return 0;
        return sound.clip.length;
    }

    public void SetVolume(string name, float volume)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.volume = volume;
    }

    public float GetVolume(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return -1;
        return sound.source.volume;
    }

    public bool IsSoundOn(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null) return false;
        return sound.source.isPlaying;
    }
}
