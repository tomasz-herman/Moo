using Assets.Scripts.SoundManager;
using UnityEngine;

public class MusicRoom : MonoBehaviour
{
    public SoundTypeWithPlaybackSettings Sound;

    [HideInInspector]
    public Audio Audio;

    public bool IsInside { get; private set; } 

    public int ColliderCount { get; private set; }

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        Audio = _audioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        ColliderCount++;
        UpdateState();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        ColliderCount--;
        UpdateState();
    }

    private void UpdateState()
    {
        if (ColliderCount == 0 && IsInside)
        {
            IsInside = false;

            Audio.Pause();
            //TODO: add there way to play other music or invoke some event handler
        }
        else if (ColliderCount > 0 && !IsInside)
        {
            IsInside = true;

            if (!Audio.IsPlaying)
            {
                Audio.Play();
            }
            else
            {
                Audio.UnPause();
            }

            //TODO: add there way to pause other music or invoke some event handler
        }
    }
}
