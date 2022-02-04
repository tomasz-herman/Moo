using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    public GameWorld gameWorld;

    private bool leftClicked = false;

    private GameObject gun;

    void Start()
    {
        shooting = GetComponent<Shooting>();

        gun = GameObject.Find("Gun");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameWorld.IsPaused())
            leftClicked = true;
        if (Input.GetMouseButtonUp(0))
            leftClicked = false;

        if (leftClicked)
        {
            shooting.TryShoot(gameObject, gun.transform.position, gameObject.transform.forward);
        }

        if (Input.mouseScrollDelta.y > 0) shooting.NextWeapon();
        else if (Input.mouseScrollDelta.y < 0) shooting.PrevWeapon();
    }
}
