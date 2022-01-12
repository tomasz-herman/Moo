using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorCenterScript : MonoBehaviour
{
    [SerializeField] private Corridor Corridor;

    private void OnTriggerEnter(Collider other)
    {
        Corridor.CorridorCenterPassed.Invoke();
    }
}
