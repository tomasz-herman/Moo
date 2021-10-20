using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = 1f;
    private float verticalSpeed = 0;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
            verticalSpeed = 0;
        else
            verticalSpeed += gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, -verticalSpeed, 0));
    }
}
