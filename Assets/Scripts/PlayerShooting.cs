using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;
    void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0) shooting.NextWeapon();
        else if (Input.mouseScrollDelta.y < 0) shooting.PrevWeapon();

        if (Input.GetMouseButton(0))
        {
            shooting.TryShoot(gameObject, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.forward);
        }
    }
}
