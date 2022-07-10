using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup audioMixer;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.5f, 2f)]
    public float pitch = 1;

    public bool loop;

    [Range(0f, 1f)]
    public float SpatialBlend = 0f;

    [Range(0f, 1.1f)]
    public float reverZoneMix = 1f;

    [Space(20)]
    [Header("3D Sound Settings")]
    
    [Range(0f, 5f)]
    public float dopplerLevel = 1f;

    [Range(0f, 360f)]
    public float spread = 0;

    public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

    public float minDistance = 1f;

    public float MaxDistance = 500f;

    [HideInInspector]
    public AudioSource source;


}
