using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [HideInInspector] public int UpgradeCount = 1;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.Upgrade(UpgradeCount);
            Destroy(gameObject);
        }
    }
}
