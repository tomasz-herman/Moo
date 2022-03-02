using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSolover : MonoBehaviour
{
    [HideInInspector] public Vector3 Offset;

    private void FixedUpdate()
    {
        gameObject.transform.position = gameObject.transform.position + Offset;
    }
}
