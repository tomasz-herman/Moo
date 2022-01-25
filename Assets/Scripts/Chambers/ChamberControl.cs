using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
    [HideInInspector] public List<SpawnLocationScript> SpawnLocations;
    [HideInInspector] public PathSymbolControler symbol;

    private void Awake()
    {
        SpawnLocations = GetComponentsInChildren<SpawnLocationScript>().ToList();
        symbol = gameObject.GetComponentInChildren<PathSymbolControler>();
    }

    public void SetFightPathsColors()
    {
        symbol.SetMaterials(PathMatirials.GetMaterialFromType(PathTypes.Fight));
    }
    public void SetDefaultPathsColors()
    {
        symbol.SetDefaultMaterials();
    }
    public void SetNonActivePathsColors()
    {
        symbol.SetMaterials(PathMatirials.GetMaterialFromType(PathTypes.None));
    }
}
