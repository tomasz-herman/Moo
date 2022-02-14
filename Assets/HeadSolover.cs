using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSolover : MonoBehaviour
{
    private float headHaight;
    private void Start()
    {
        headHaight = gameObject.transform.position.y;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = gameObject.transform.position - Vector3.up * gameObject.transform.position.y + Vector3.up * headHaight;
    }
}
