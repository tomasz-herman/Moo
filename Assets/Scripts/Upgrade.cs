using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.Upgrade();
            Destroy(gameObject);
        }
    }
}
