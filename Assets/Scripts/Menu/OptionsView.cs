using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsView : MenuView
{
    [SerializeField] private AudioMixer musicMixer, soundMixer, uiSoundMixer;

    [SerializeField] private TMP_Dropdown qualityDropdown, resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private List<Resolution> resolutions;

    protected override void Awake()
    {
        base.Awake();

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions.ToList();
        resolutions.Reverse();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());
        Resolution current = Screen.currentResolution;
        for(int i = 0; i < resolutions.Count; i++)
        {
            Resolution res = resolutions[i];
            if(current.width == res.width && current.height == res.height)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetSoundVolume(float volume)
    {
        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetUISoundVolume(float volume)
    {
        //TODO: add slider in UI
        uiSoundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetResolution(int resIdx)
    {
        Resolution resolution = resolutions[resIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Resolution resolution = Screen.currentResolution;
        Screen.SetResolution(resolution.width, resolution.height, fullscreen, resolution.refreshRate);
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
