using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathMatirialsContainer", menuName = "ScriptableObjects/PathMatirialsContainer")]
public class PathMatirialsContainer : ScriptableObject
{
    [SerializeField] private List<PathMaterialInfo> Materials;
    [HideInInspector] public Dictionary<PathTypes, Material> MaterialsDict = new Dictionary<PathTypes, Material>();

    private void OnValidate()
    {
        MaterialsDict.Clear();
        foreach (var item in Materials)
        {
            MaterialsDict.Add(item.type, item.material);
        }
    }
}

public enum PathTypes { Main, None, Optional, Fight}

[System.Serializable]
public struct PathMaterialInfo
{
    public Material material;
    public PathTypes type;
}

public static class PathMatirials
{
    private static Dictionary<PathTypes, Material> Materials = new Dictionary<PathTypes, Material>();
    public static Material GetMaterialFromType(PathTypes type)
    {
        if(Materials.Count==0)
        {
            PathMatirialsContainer Container = Resources.Load<PathMatirialsContainer>("ScriptableObjects/PathMatirials");
            Materials = Container.MaterialsDict;
        }
        return Materials[type];
    }
}