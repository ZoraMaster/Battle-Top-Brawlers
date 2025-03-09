using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }
    private void Load()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}
