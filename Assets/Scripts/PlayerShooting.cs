using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    public GameWorld gameWorld;

    private bool leftClicked = false;

    void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameWorld.IsPaused())
            leftClicked = true;
        if (Input.GetMouseButtonUp(0))
            leftClicked = false;
        
        if(leftClicked)
        {
           shooting.TryShoot(gameObject, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.forward);
        }
    }
}
