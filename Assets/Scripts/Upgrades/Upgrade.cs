using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [HideInInspector] public int upgradeCount = 1;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.Upgrade(upgradeCount);
            Destroy(gameObject);
        }
    }
}
