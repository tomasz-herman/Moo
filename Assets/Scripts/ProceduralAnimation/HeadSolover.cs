using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSolover : MonoBehaviour
{
    [HideInInspector] public Vector3 Offset;
    float haight;
    private void Start()
    {
        haight = gameObject.transform.position.y;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, haight, gameObject.transform.position.z) + Offset;
    }
}
