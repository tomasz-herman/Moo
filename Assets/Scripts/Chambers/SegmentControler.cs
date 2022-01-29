using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentControler : MonoBehaviour
{
    public Direction direction;
    List<BlocadeTransition> SegmentObjects = new List<BlocadeTransition>();
    List<PathSegment> Path = new List<PathSegment>();
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject ob = transform.GetChild(i).gameObject;
            PathSegment pathsegment = ob.GetComponent<PathSegment>();
            if (pathsegment != null)
                Path.Add(pathsegment);
            else
                SegmentObjects.Add(ob.GetComponent<BlocadeTransition>());
        }
    }

    public void SetPathBlocade(bool isActive)
    {
        foreach (var item in SegmentObjects)
        {
            item.gameObject.SetActive(isActive==item.IsBlocade); // or (!(isActive^item.IsBlocade))
        }
        foreach (var item in Path)
        {
            item.gameObject.SetActive(!isActive);
        }
    }

    public void SetPathMaterial(Material material)
    {
        foreach (var item in Path)
        {
            item.SetMaterial(material);
        }
    }
}
