using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovement : MonoBehaviour
{
    [SerializeField] float StepLong;
    [SerializeField] float StepHaight;
    [SerializeField] float SpeedMultiplayer;
    [SerializeField] float BodyMovementMultiplayer;
    [SerializeField] float SideOffsetStanding;
    [SerializeField] float SideOffsetMoving;
    [SerializeField] List<LegSolver> LegTargets = new List<LegSolver>();
    [SerializeField] HeadSolover HeadTarget;
    [SerializeField] Transform RootTransform;
    PlayerMovement playerMovement;
    Rigidbody rigidbody;
    LegSolver CurrentLeg = null;
    Vector3 oldPosition;
    Vector3 nextPosition;
    private float lerp = 0;
    private float rootHaight;
    private Ray currentRay;
    private Ray predictionRay;
    private float currentSideOffset;
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rootHaight = RootTransform.position.y;
    }

    private void FixedUpdate()
    {
        currentSideOffset = playerMovement.direction.magnitude > 0 ? SideOffsetMoving : SideOffsetStanding;
        if (Physics.Raycast(new Ray(rigidbody.transform.position + Vector3.up, Vector3.down), out RaycastHit info, 10, LayerMask.GetMask(Layers.Floor)))
        {
            currentRay = new Ray(info.point, rigidbody.transform.right);
            predictionRay = new Ray(currentRay.origin + StepLong * playerMovement.direction, currentRay.direction);
            foreach (var item in LegTargets)
            {
                item.currentPosition = predictionRay.origin + predictionRay.direction * currentSideOffset * item.getDirection();
            }
        }
        //if (Physics.Raycast(new Ray(gameObject.transform.position + Vector3.up, Vector3.down), out RaycastHit info, 10, LayerMask.GetMask(Layers.Floor)))
        //    foreach (var item in LegTargets)
        //    {
        //        if (CurrentLeg == null)
        //        {
        //            if (Mathf.Abs(Vector3.Distance(item.currentPosition, info.point)) > StepTreshold)
        //            {
        //                Ray ray = new Ray(gameObject.transform.position + rigidbody.velocity * StepLong + item.getDirection(gameObject.transform) * SideOffset + Vector3.up, Vector3.down);
        //                if (Physics.Raycast(ray, out RaycastHit info2, 10, LayerMask.GetMask(Layers.Floor)))
        //                {
        //                    CurrentLeg = item;
        //                    lerp = 0;
        //                    oldPosition = CurrentLeg.currentPosition;
        //                    nextPosition = info2.point;
        //                }
        //            }
        //        }
        //    }
        //if (CurrentLeg != null)
        //{
        //    if (lerp < 1)
        //    {
        //        Vector3 footPosition = Vector3.Lerp(oldPosition, nextPosition, lerp);
        //        footPosition.y += Mathf.Sin(lerp * Mathf.PI) * StepHaight;
        //        RootTransform.position = new Vector3(RootTransform.position.x, rootHaight + Mathf.Sin(lerp * Mathf.PI) * StepHaight * BodyMovementMultiplayer, RootTransform.position.z);

        //        lerp += Time.fixedDeltaTime * rigidbody.velocity.magnitude * SpeedMultiplayer;
        //        CurrentLeg.currentPosition = footPosition;
        //    }
        //    else
        //    {
        //        CurrentLeg = null;
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(currentRay.origin - currentRay.direction * SideOffsetStanding, currentRay.origin + currentRay.direction * SideOffsetStanding);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(predictionRay.origin - predictionRay.direction * currentSideOffset, predictionRay.origin + predictionRay.direction * currentSideOffset);
    }
}
