using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovementEnemy : ProceduralMovement
{
    [SerializeField] float footHaight = 0.2f;
    SimpleEnemyAI enemyAI;
    protected override void MovementStart()
    {
        enemyAI = gameObject.GetComponent<SimpleEnemyAI>();
    }

    protected override Vector3 GetDirection()
    {
        return enemyAI.movementDirection;
    }

    protected override float FootHaight()
    {
        return footHaight;
    }
}
