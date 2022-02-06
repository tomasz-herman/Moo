using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;

    void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (GameWorld.IsPaused()) return;
        
        if (Mouse.current.leftButton.isPressed)
        {
            shooting.TryShoot(gameObject, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.forward);
        }

        if (Mouse.current.scroll.y.ReadValue() > 0) shooting.NextWeapon();
        else if (Mouse.current.scroll.y.ReadValue() < 0) shooting.PrevWeapon();
    }
}
