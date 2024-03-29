using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public UnityEvent<bool> MoveStopEvent = new UnityEvent<bool>();
    void Awake()
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
        MoveStopEvent.Invoke(animator.GetBool("DoorClose"));
    }

    public bool IsOpen()
    {
        return !animator.GetBool("DoorMove") && !animator.GetBool("DoorClose");
    }

    public bool IsClosed()
    {
        return !animator.GetBool("DoorMove") && animator.GetBool("DoorClose");
    }
}
