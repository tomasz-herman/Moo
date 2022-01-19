using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
    [HideInInspector] public List<SpawnLocationScript> SpawnLocations;
    // Start is called before the first frame update
    private void Awake()
    {
        SpawnLocations = GetComponentsInChildren<SpawnLocationScript>().ToList();
    }
}
