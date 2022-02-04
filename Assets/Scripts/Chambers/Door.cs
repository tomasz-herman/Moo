using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
        animator.SetBool("DoorClose", true);
        animator.SetBool("DoorMove", true);
    }

    public void OpenDoor()
    {
        animator.SetBool("DoorClose", false);
        animator.SetBool("DoorMove", true);
    }

    public void MoveOff()
    {
        animator.SetBool("DoorMove", false);
    }
}
