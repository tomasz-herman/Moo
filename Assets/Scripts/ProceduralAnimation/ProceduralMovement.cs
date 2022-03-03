using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralMovement : MonoBehaviour
{
    [SerializeField] float StepLong;
    [SerializeField] float StepHaight;
    [SerializeField] float StepSpeed;
    [SerializeField] float StepTooLong;
    [SerializeField] float SideOffsetStanding;
    [SerializeField] float SideOffsetMoving;
    [SerializeField] float StendingMinOffset;
    [SerializeField] float IdleSpeed;
    [SerializeField] float HaightOffset;
    [SerializeField] float HeadMovementMultiplayer;
    [SerializeField] List<LegSolver> LegTargets = new List<LegSolver>();
    [SerializeField] HeadSolover HeadTarget;
    [SerializeField] Transform RootTransform;
    private Vector3 rootLocalPosition;
    private Ray currentRightRay;
    private Ray currentForwardRay;
    private Ray predictionRay;
    private float currentSideOffset;
    private bool moving = false;
    private float lerp = 0;
    void Start()
    {
        MovementStart();
        rootLocalPosition = RootTransform.localPosition - Vector3.up * HaightOffset;
        UpdateSteps();
    }

    private void FixedUpdate()
    {
        Vector3 direction = GetDirection();
        currentSideOffset = direction.magnitude > 0 ? SideOffsetMoving : SideOffsetStanding;
        if (Physics.Raycast(new Ray(gameObject.transform.position + Vector3.up, Vector3.down), out RaycastHit info, 10, LayerMask.GetMask(Layers.Floor)))
        {
            currentRightRay = new Ray(info.point, gameObject.transform.right);
            currentForwardRay = new Ray(currentRightRay.origin, gameObject.transform.forward);
            predictionRay = new Ray(currentRightRay.origin + StepLong * direction, currentRightRay.direction);
            Ray ray = GetTestRay(direction);
            if (direction.magnitude > 0) //moving
            {
                float max = 0;
                LegSolver further = null;
                foreach (var item in LegTargets)
                {
                    float distance = Vector3.Cross(ray.direction, item.currentPosition - ray.origin).magnitude;
                    if (distance > max && (distance > StepTooLong || !moving))
                    {
                        max = distance;
                        further = item;
                    }
                }

                if (further != null)
                    if (further.IsGrounded())
                        if (Other(further).IsGrounded())
                            further.Step(predictionRay.origin + predictionRay.direction * currentSideOffset * further.getDirection() + Vector3.up * FootHaight());
                moving = true;
            }
            else // rotating in place
            {
                Vector3 legline = LegTargets[1].currentPosition - LegTargets[0].currentPosition;
                Vector3 legcenter = LegTargets[1].currentPosition + legline / 2;
                float angle = Vector3.Angle(legline, currentRightRay.direction);
                if (angle > 90f)
                    angle = 180f - angle;
                if (angle > 45f || legline.magnitude < SideOffsetStanding * 2 * StendingMinOffset || legline.magnitude > SideOffsetStanding * 2 * 1.1f || (legcenter - currentRightRay.origin).magnitude > SideOffsetStanding * 5)
                {
                    float max = 0;
                    LegSolver further = null;
                    foreach (var item in LegTargets)
                    {
                        float distance = Vector3.Cross(currentRightRay.direction, item.currentPosition - currentRightRay.origin).magnitude;
                        if (distance > max)
                        {
                            max = distance;
                            further = item;
                        }
                    }

                    if (further.IsGrounded())
                        if (Other(further).IsGrounded())
                            further.Step(predictionRay.origin + predictionRay.direction * currentSideOffset * further.getDirection() + Vector3.up * FootHaight());
                }
                moving = false;
            }

            foreach (var item in LegTargets)
            {
                if (!item.IsGrounded())
                    lerp = item.lerp;
            }
        }

        float sin = Mathf.Sin(lerp * Mathf.PI);
        RootTransform.localPosition = rootLocalPosition + Vector3.up * sin * HaightOffset;
        HeadTarget.Offset = Vector3.up * sin * HaightOffset * HeadMovementMultiplayer;
        lerp += Time.fixedDeltaTime * IdleSpeed;
        if (lerp >= 1)
            lerp = 0;
    }

    protected abstract Vector3 GetDirection();
    protected abstract void MovementStart();
    protected abstract float FootHaight();

    private Ray GetTestRay(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, currentForwardRay.direction);
        if (angle > 90f)
            angle = 180f - angle;
        if (angle > 45f)
            return currentForwardRay;
        return currentRightRay;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(currentRightRay.origin - currentRightRay.direction * SideOffsetStanding, currentRightRay.origin + currentRightRay.direction * SideOffsetStanding);
        Gizmos.DrawLine(currentForwardRay.origin - currentForwardRay.direction * SideOffsetStanding, currentForwardRay.origin + currentForwardRay.direction * SideOffsetStanding);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(predictionRay.origin - predictionRay.direction * currentSideOffset, predictionRay.origin + predictionRay.direction * currentSideOffset);
    }

    private void UpdateSteps()
    {
        foreach (var item in LegTargets)
            item.UpdateHaightSpeed(StepHaight, StepSpeed);
    }

    private void OnValidate()
    {
        UpdateSteps();
    }

    private LegSolver Other(LegSolver leg)
    {
        foreach (var item in LegTargets)
            if (item != leg)
                return item;
        return null;
    }
}
