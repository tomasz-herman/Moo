using UnityEngine;

public class PlayerAudioListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //This is to remove warning with lack of audio listener in the scene.
        var followCamera = FindObjectOfType(typeof(Camera)) as Camera;
        if (followCamera != null)
        {
            var audioListener = followCamera.GetComponent<AudioListener>();
            Destroy(audioListener);
        }
    }
}
