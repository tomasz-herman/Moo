using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
    [HideInInspector] public List<SpawnLocationScript> SpawnLocations;

    private void Awake()
    {
        SpawnLocations = GetComponentsInChildren<SpawnLocationScript>().ToList();
    }
}
