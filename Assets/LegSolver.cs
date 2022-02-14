using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSolver : MonoBehaviour
{
    [HideInInspector] public Vector3 currentPosition;
    [SerializeField] bool isRight;
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
        Gizmos.DrawSphere(currentPosition, 0.5f);
    }

    public Vector3 getDirection(Transform root)
    {
        if (isRight)
            return root.transform.right;
        return -root.transform.right;
    }
}
