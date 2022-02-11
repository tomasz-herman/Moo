using Assets.Scripts.SoundManager;
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

    private bool awaken = false;

    protected override void Awake()
    {
        base.Awake();

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());

        resolutions = Screen.resolutions.ToList();
        resolutions.Reverse();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());

        awaken = true;
    }

    public void SetEffectsVolume(float volume)
    {
        if(awaken)
            AudioManager.Instance.SoundVolume = CalculateMixerVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (awaken)
            AudioManager.Instance.MusicVolume = CalculateMixerVolume(volume);
    }

    public void SetUiVolume(float volume)
    {
        if (awaken)
            AudioManager.Instance.UiVolume = CalculateMixerVolume(volume);
    }

    private void OnEnable()
    {
        uiSlider.value = CalculateSliderValueFromMixerVolume(AudioManager.Instance.UiVolume);
        soundSlider.value = CalculateSliderValueFromMixerVolume(AudioManager.Instance.SoundVolume);
        musicSlider.value = CalculateSliderValueFromMixerVolume(AudioManager.Instance.MusicVolume);


        fullscreenToggle.isOn = Screen.fullScreen;

        int currentWidth = Screen.width;
        int currentHeight = Screen.height;
        for (int i = 0; i < resolutions.Count; i++)
        {
            Resolution res = resolutions[i];
            if (currentWidth == res.width && currentHeight == res.height)
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
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, resolution.refreshRate);
    }

    public void SetFullscreen(bool fullscreen)
    {
        int width = Screen.width;
        int height = Screen.height;
        Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }

    public void SetQuality(int qualityIdx)
    {
        QualitySettings.SetQualityLevel(qualityIdx);
        Config.Entry.graphicsQuality = QualitySettings.names[qualityIdx];
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
