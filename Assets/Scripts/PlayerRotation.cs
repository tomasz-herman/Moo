using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    //private int mouseRaycastLayerMask;

    void Start()
    {
        //mouseRaycastLayerMask = LayerMask.GetMask(Layers.Enemy, Layers.Floor, Layers.Wall);
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    void Update()
    {
        if (GameWorld.IsPaused()) return;
        
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var plane = new Plane(Vector3.up, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 lookAt = ray.GetPoint(distance);
            gameObject.transform.LookAt(lookAt);
        }
    }
}
