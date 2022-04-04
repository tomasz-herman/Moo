using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSolver : MonoBehaviour
{
    [SerializeField] Transform AnchorPoint;

    private void FixedUpdate()
    {
        gameObject.transform.position = AnchorPoint.position;
    }

}
