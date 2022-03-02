using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSolver : MonoBehaviour
{
    [HideInInspector] public Vector3 currentPosition;
    [SerializeField] bool isRight;
    [SerializeField] bool DrawGizmos = false;
    void Start()
    {
        currentPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = currentPosition;
    }

    private void OnDrawGizmos()
    {
        if (!DrawGizmos)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPosition, 0.2f);
    }

    public float getDirection()
    {
        if (isRight)
            return 1;
        return -1;
    }
}
