using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovementPlayer : ProceduralMovement
{
    PlayerMovement playerMovement;
    protected override void MovementStart()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    protected override Vector3 GetDirection()
    {
        return playerMovement.direction;
    }
}
