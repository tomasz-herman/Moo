using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovementEnemy : ProceduralMovement
{
    SimpleEnemyAI enemyAI;
    protected override void MovementStart()
    {
        enemyAI = gameObject.GetComponent<SimpleEnemyAI>();
    }

    protected override Vector3 GetDirection()
    {
        return enemyAI.movementDirection;
    }
}
