using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentControler : MonoBehaviour
{
    public Direction direction;
    List<BlocadeTransition> SegmentObjects = new List<BlocadeTransition>();
    List<PathSegment> Path = new List<PathSegment>();
    private void Awake()
    {
        Path = gameObject.GetComponentsInChildren<PathSegment>().ToList();
        SegmentObjects = gameObject.GetComponentsInChildren<BlocadeTransition>().ToList();
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
