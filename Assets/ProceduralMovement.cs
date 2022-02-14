using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovement : MonoBehaviour
{
    [SerializeField] float StepTreshold;
    [SerializeField] float StepLong;
    [SerializeField] float StepHaight;
    [SerializeField] float SpeedMultiplayer;
    [SerializeField] float BodyMovementMultiplayer;
    [SerializeField] float SideOffset;
    [SerializeField] List<LegSolver> LegTargets = new List<LegSolver>();
    [SerializeField] Transform RootTransform;
    PlayerMovement playerMovement;
    PlayerRotation playerRotation;
    LegSolver CurrentLeg = null;
    Vector3 oldPosition;
    Vector3 nextPosition;
    private float lerp = 0;
    private float rootHaight;
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerRotation = gameObject.GetComponent<PlayerRotation>();
        rootHaight = RootTransform.position.y;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(gameObject.transform.position + Vector3.up, Vector3.down), out RaycastHit info, 10, LayerMask.GetMask(Layers.Floor)))
            foreach (var item in LegTargets)
            {
                if (CurrentLeg == null)
                {
                    if (Mathf.Abs(Vector3.Distance(item.currentPosition, info.point)) > StepTreshold)
                    {
                        Ray ray = new Ray(gameObject.transform.position + playerMovement.direction * StepLong + item.getDirection(gameObject.transform) * SideOffset + Vector3.up, Vector3.down);
                        if (Physics.Raycast(ray, out RaycastHit info2, 10, LayerMask.GetMask(Layers.Floor)))
                        {
                            CurrentLeg = item;
                            lerp = 0;
                            oldPosition = CurrentLeg.currentPosition;
                            nextPosition = info2.point;
                        }
                    }
                }
            }
        if (CurrentLeg != null)
        {
            if (lerp < 1)
            {
                Vector3 footPosition = Vector3.Lerp(oldPosition, nextPosition, lerp);
                footPosition.y += Mathf.Sin(lerp * Mathf.PI) * StepHaight;
                RootTransform.position = new Vector3(RootTransform.position.x, rootHaight + Mathf.Sin(lerp * Mathf.PI) * StepHaight * BodyMovementMultiplayer, RootTransform.position.z);

                lerp += Time.fixedDeltaTime * playerMovement.Speed * SpeedMultiplayer;
                CurrentLeg.currentPosition = footPosition;
            }
            else
            {
                CurrentLeg = null;
            }
        }
    }
}
