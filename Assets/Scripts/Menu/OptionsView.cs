using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsView : MenuView
{
    [SerializeField] private AudioMixer musicMixer, soundMixer;

    public void SetEffectsVolume(float volume)
    {
        soundMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", volume);
    }

    public void SetResolution(int resIdx)
    {

    }

    public void SetFullscreen(bool fullscreen)
    {

    }

    public void SetQuality(int qualityIdx)
    {

    }

    public void Continue()
    {
        Menu.ShowMainMenu();
    }
}
