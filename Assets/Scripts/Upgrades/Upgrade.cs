using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [HideInInspector] public int upgradeCount = 1;
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
        transform.Rotate(0, Utils.FloatBetween(0, 360), 0);
        
    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }
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
