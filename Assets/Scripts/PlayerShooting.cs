using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    private bool isShooting = false;

    void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (GameWorld.IsPaused()) return;
        
        if (isShooting)
        {
            shooting.TryShoot(gameObject, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.forward);
        }
    }

    void OnSelectWeapon(InputValue nextWeaponValue)
    {
        float next = nextWeaponValue.Get<float>();
        
        if (next > 0) shooting.NextWeapon();
        else if (next < 0) shooting.PrevWeapon();
    }
    
    void OnFire()
    {
        isShooting = !isShooting;
    }
}
