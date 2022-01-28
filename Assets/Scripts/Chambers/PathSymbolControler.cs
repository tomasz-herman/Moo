using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSymbolControler : MonoBehaviour
{
    private Dictionary<Direction, List<Renderer>> elements = new Dictionary<Direction, List<Renderer>>();
    public Dictionary<Direction, Material> materials = new Dictionary<Direction, Material>();

    private void Awake()
    {
        foreach (Direction item in Enum.GetValues(typeof(Direction)))
        {
            elements.Add(item, new List<Renderer>());
            materials.Add(item, null);
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform t = gameObject.transform.GetChild(i);
            float margin = 0.5f;
            if (Mathf.Abs(t.localPosition.x) > margin && Mathf.Abs(t.localPosition.z) <= margin)
            {
                if (Mathf.Sign(t.localPosition.x) > 0)
                    elements[Direction.Left].Add(t.gameObject.GetComponent<Renderer>());
                else
                    elements[Direction.Right].Add(t.gameObject.GetComponent<Renderer>());
            }
            else if (Mathf.Abs(t.localPosition.z) > margin)
            {
                if (Mathf.Sign(t.localPosition.z) > 0 && Mathf.Abs(t.localPosition.x) <= margin)
                    elements[Direction.Down].Add(t.gameObject.GetComponent<Renderer>());
                else
                    elements[Direction.Up].Add(t.gameObject.GetComponent<Renderer>());
            }
        }
    }

    public void SetDefaultMaterials()
    {
        foreach (var item in materials)
        {
            foreach (var element in elements[item.Key])
            {
                element.material = item.Value;
            }
        }
    }

    public void SetMaterials(Material material)
    {
        foreach (var item in elements)
        {
            foreach (var element in elements[item.Key])
            {
                element.material = material;
            }
        }
    }

    public void SetActive(Direction direction, bool isActive)
    {
        foreach (var element in elements[direction])
        {
            element.gameObject.SetActive(isActive);
        }
    }

    public void SetBossDirection(Direction direction)
    {
        foreach (var item in elements)
        {
            if (item.Key != direction)
                item.Value[1].gameObject.SetActive(false);
        }
    }
}
