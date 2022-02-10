using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    //private int mouseRaycastLayerMask;
    public GameWorld gameWorld;
    private GameObject gun;

    void Start()
    {
        //mouseRaycastLayerMask = LayerMask.GetMask(Layers.Enemy, Layers.Floor, Layers.Wall);
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;

        gun = GameObject.Find("Gun");
    }

    void Update()
    {
        if(!gameWorld.IsPaused())
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, gun.transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 lookAt = ray.GetPoint(distance);
                lookAt.y = transform.position.y;
                gameObject.transform.LookAt(lookAt);
            }
        }
    }
}
