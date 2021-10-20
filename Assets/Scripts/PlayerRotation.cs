using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask(Layers.Floor)))
        {
            Vector3 lookAt = hit.point;
            lookAt.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(lookAt);
        }
    }
}
