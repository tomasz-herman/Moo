using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSolver : MonoBehaviour
{
    [HideInInspector] public Vector3 currentPosition;
    [SerializeField] bool isRight;
    [SerializeField] bool DrawGizmos = false;
    Vector3 oldPosition;
    Vector3 position;
    [HideInInspector] public float lerp = 1;
    float StepHaight;
    float StepSpeed;
    void Start()
    {
        currentPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, currentPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * StepHaight;

            lerp += Time.fixedDeltaTime * StepSpeed;
            position = footPosition;
        }
        gameObject.transform.position = position;
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

    public void Step(Vector3 newPosition)
    {
        oldPosition = currentPosition;
        currentPosition = newPosition;
        lerp = 0;
    }

    public bool IsGrounded()
    {
        return lerp >= 1;
    }

    public void UpdateHaightSpeed(float haight, float speed)
    {
        StepHaight = haight;
        StepSpeed = speed;
    }
}
