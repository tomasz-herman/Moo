using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsView : MenuView
{
    [SerializeField] private AudioMixer musicMixer, soundMixer, uiMixer;

    [SerializeField] private TMP_Dropdown qualityDropdown, resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider musicSlider, soundSlider, uiSlider;

    private List<Resolution> resolutions;

    private const float MixerVolumeMultiplier = 20f;

    protected override void Awake()
    {
        base.Awake();

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());

        resolutions = Screen.resolutions.ToList();
        resolutions.Reverse();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());
    }

    public void SetEffectsVolume(float volume)
    {
        soundMixer.SetFloat("volume", CalculateMixerVolume(volume));
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", CalculateMixerVolume(volume));
    }

    public void SetUiVolume(float volume)
    {
        uiMixer.SetFloat("volume", CalculateMixerVolume(volume));
    }

    private void OnEnable()
    {
        float volume;

        uiMixer.GetFloat("volume", out volume);
        uiSlider.value = CalculateSliderValueFromMixerVolume(volume);

        musicMixer.GetFloat("volume", out volume);
        musicSlider.value = CalculateSliderValueFromMixerVolume(volume);

        soundMixer.GetFloat("volume", out volume);
        soundSlider.value = CalculateSliderValueFromMixerVolume(volume);

        fullscreenToggle.isOn = Screen.fullScreen;

        Resolution current = Screen.currentResolution;
        for (int i = 0; i < resolutions.Count; i++)
        {
            Resolution res = resolutions[i];
            if (current.width == res.width && current.height == res.height)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        qualityDropdown.value = QualitySettings.GetQualityLevel();
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

    private static float CalculateMixerVolume(float volume)
    {
        return Mathf.Log10(volume) * MixerVolumeMultiplier;
    }

    private static float CalculateSliderValueFromMixerVolume(float volume)
    {
        return Mathf.Pow(10f, volume / MixerVolumeMultiplier);
    }
}
