using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class PlayerAudioListener : MonoBehaviour
    {
        public GameObject Owner;

        [HideInInspector]
        public AudioListener AudioListener;

        private void Start()
        {
            var audioListeners = FindObjectsOfType<AudioListener>();
            foreach (var audioListener in audioListeners)
            {
                audioListener.enabled = false;
            }

            AudioListener = GetComponent<AudioListener>();
            AudioListener.enabled = true;
        }

        private void Update()
        {
            transform.position = Owner.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}
