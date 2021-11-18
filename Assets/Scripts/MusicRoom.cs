using UnityEngine;

public class MusicRoom : MonoBehaviour
{
    public bool IsInside;
    public int ColliderCount;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            ColliderCount++;
            UpdateState();            
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            ColliderCount--;
            UpdateState();
        }
    }

    private void UpdateState()
    {
        if (ColliderCount == 0 && IsInside)
        {
            IsInside = false;

            var audioManager = FindObjectOfType<AudioManager>();
            audioManager?.Pause(Assets.Scripts.SoundManager.SoundType.MusicRoom);
            audioManager?.Play(Assets.Scripts.SoundManager.SoundType.BackgroundTheme);
        }
        else if(ColliderCount > 0 && !IsInside)
        {
            IsInside = true;

            var audioManager = FindObjectOfType<AudioManager>();
            audioManager?.Pause(Assets.Scripts.SoundManager.SoundType.BackgroundTheme);
            audioManager?.Play(Assets.Scripts.SoundManager.SoundType.MusicRoom);
        }
    }
}
