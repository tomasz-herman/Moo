using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (gameWorld.IsPaused()) return;

        if (Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            shooting.TryShoot(gameObject, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.forward);
        }

        if (Input.mouseScrollDelta.y > 0) shooting.NextWeapon();
        else if (Input.mouseScrollDelta.y < 0) shooting.PrevWeapon();
    }
}
