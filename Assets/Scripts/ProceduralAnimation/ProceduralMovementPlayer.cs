using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovementPlayer : ProceduralMovement
{
    PlayerMovement playerMovement;
    float footHaight = 0;
    protected override void MovementStart()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    protected override Vector3 GetDirection()
    {
        return playerMovement.direction;
    }

    protected override float FootHaight()
    {
        return footHaight;
    }
}
