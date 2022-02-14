using Assets.Scripts.SoundManager;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public SoundTypeWithPlaybackSettings ResumeButtonSound;
    public SoundTypeWithPlaybackSettings OptionsButtonSound;
    public SoundTypeWithPlaybackSettings QuitButtonSound;
    
    [HideInInspector]
    public Audio ResumeButtonAudio;
    [HideInInspector]
    public Audio OptionsButtonAudio;
    [HideInInspector]
    public Audio QuitButtonAudio;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        this.ResumeButtonAudio = _audioManager.CreateUISound(this.ResumeButtonSound.SoundType, this.ResumeButtonSound.PlaybackSettings);
        this.OptionsButtonAudio = _audioManager.CreateUISound(this.OptionsButtonSound.SoundType, this.OptionsButtonSound.PlaybackSettings);
        this.QuitButtonAudio = _audioManager.CreateUISound(this.QuitButtonSound.SoundType, this.QuitButtonSound.PlaybackSettings);
    }
    public void ResumeButtonPlaySound()
    {
        this.ResumeButtonAudio?.Play();
    }

    public void OptionsButtonPlaySound()
    {
        this.OptionsButtonAudio?.Play();
    }

    public void QuitButtonPlaySound()
    {
        this.QuitButtonAudio?.Play();
    }
}
