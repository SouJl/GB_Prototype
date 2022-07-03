using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private float _musicVolume = 0.5f;
    private float _sfxVolume = 0.8f;

    void Start()
    {

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        PlayerPrefs.SetFloat("SFXVolume", _sfxVolume);

        musicVolumeSlider.value = _musicVolume;
        sfxVolumeSlider.value = _sfxVolume;
    }

    public void CloseOpionsMenu()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
}
