using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    public float TriggerRadius = 8;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, TriggerRadius);
    }
}
