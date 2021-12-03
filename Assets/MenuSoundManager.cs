using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer SFXMixer, musicMixer;
    [SerializeField] SoundSettings soundSettings;
    [SerializeField] Slider musicSilder, SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(soundSettings.musicVolume)*20);
        SFXMixer.SetFloat("SFXVol", Mathf.Log10(soundSettings.musicVolume)*20);
        musicSilder.value = soundSettings.musicVolume;
        SFXSlider.value = soundSettings.SetSFXVolume;

    }

    public void SetMenuSFX(float sliderValue)
    {
        SFXMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
        soundSettings.SetSFXVolume = sliderValue;
    }

    public void SetMenuMusic(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
        soundSettings.musicVolume = sliderValue;
    }
}
