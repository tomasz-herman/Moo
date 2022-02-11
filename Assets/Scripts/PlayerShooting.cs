using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    public GameWorld gameWorld;

    private GameObject gun;

    void Start()
    {
        shooting = GetComponent<Shooting>();

        gun = GameObject.Find("Gun");
    }

    void Update()
    {
        if (gameWorld.IsPaused()) return;

        if (Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            shooting.TryShoot(gameObject, gun.transform.position, gameObject.transform.forward);
        }

        if (Input.mouseScrollDelta.y > 0) shooting.NextWeapon();
        else if (Input.mouseScrollDelta.y < 0) shooting.PrevWeapon();
    }
}
