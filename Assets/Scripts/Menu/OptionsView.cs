using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsView : MenuView
{
    [SerializeField] private AudioMixer musicMixer, soundMixer;

    [SerializeField] private TMP_Dropdown qualityDropdown, resolutionDropdown;
    protected override void Awake()
    {
        base.Awake();

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

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
        QualitySettings.SetQualityLevel(qualityIdx);
    }

    public void Continue()
    {
        Menu.ShowMainMenu();
    }
}
