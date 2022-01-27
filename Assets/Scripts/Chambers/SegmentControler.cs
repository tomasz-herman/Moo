using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentControler : MonoBehaviour
{
    public Direction direction;
    List<GameObject> SegmentObjects = new List<GameObject>();
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
                SegmentObjects.Add(ob);
        }
    }

    public void SetActive(bool isAcctive)
    {
        foreach (var item in SegmentObjects)
        {
            item.SetActive(isAcctive);
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
