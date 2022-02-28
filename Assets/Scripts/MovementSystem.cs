using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementSystem : MonoBehaviour
{
    public float defaultMovementSpeed;
    public abstract float Speed { get; set; }
}
