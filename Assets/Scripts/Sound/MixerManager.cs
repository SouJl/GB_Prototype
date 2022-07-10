using System;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mainMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    public void SetMasterVolume(float volumeValue) 
    {
        mainMixer.SetFloat("", MathF.Log10(volumeValue) * 20f);
    }

    public void SetMusicVolume(float volumeValue)
    {
        mainMixer.SetFloat("MusicVolume", MathF.Log10(volumeValue) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", volumeValue);
    }

    public void SetSFXVolume(float volumeValue)
    {
        mainMixer.SetFloat("SFXVolume", MathF.Log10(volumeValue) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", volumeValue);
    }
}
